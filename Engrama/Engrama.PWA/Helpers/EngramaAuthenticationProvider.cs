using Engrama.PWA.Areas.LoginArea.Utiles;
using Engrama.Share.Objetos;

using EngramaCoreStandar.Extensions;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Engrama.PWA.Helpers
{
	public class EngramaAuthenticationProvider : AuthenticationStateProvider, ILogginService
	{
		private readonly UserSession Session;

		private readonly IJSRuntime js;
		private readonly HttpClient httpClient;
		private readonly string TOKENKEY = "TOKENKEY";
		private readonly string EXPIRATIONTOKENKEY = "EXPIRATIONTOKENKEY";

		private AuthenticationState Anonymous =>
			new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

		public EngramaAuthenticationProvider(IJSRuntime js, HttpClient httpClient, UserSession userSession)
		{
			this.js = js;
			this.httpClient = httpClient;
			Session = userSession;

		}

		public async override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var token = await js.GetFromLocalStorage(TOKENKEY);

			if (string.IsNullOrEmpty(token))
			{
				Session.iId = -1;
				return Anonymous;
			}

			var expirationTimeString = await js.GetFromLocalStorage(EXPIRATIONTOKENKEY);
			DateTime expirationTime;

			if (DateTime.TryParse(expirationTimeString, out expirationTime))
			{
				if (IsTokenExpired(expirationTime))
				{
					await CleanUp();
					return Anonymous;
				}


			}

			return BuildAuthenticationState(token);
		}




		private bool IsTokenExpired(DateTime expirationTime)
		{
			return expirationTime <= DateTime.UtcNow;
		}

		public AuthenticationState BuildAuthenticationState(string token)
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
			return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
		}

		private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
		{
			var claims = new List<Claim>();
			var payload = jwt.Split('.')[1];
			var jsonBytes = ParseBase64WithoutPadding(payload);
			var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

			Session.iId = keyValuePairs["id"].ToInt();
			Session.Rol = keyValuePairs[ClaimTypes.Role].ToString();


			claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
			return claims;
		}

		private byte[] ParseBase64WithoutPadding(string base64)
		{
			switch (base64.Length % 4)
			{
				case 2: base64 += "=="; break;
				case 3: base64 += "="; break;
			}
			return Convert.FromBase64String(base64);
		}

		public async Task Login(AuthenticationModel userToken)
		{
			await js.SetInLocalStorage(TOKENKEY, userToken.vchToken);
			await js.SetInLocalStorage(EXPIRATIONTOKENKEY, userToken.dtExpiration.ToString());
			var authState = BuildAuthenticationState(userToken.vchToken);
			NotifyAuthenticationStateChanged(Task.FromResult(authState));
		}

		public async Task Logout()
		{
			await CleanUp();
			NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
		}

		private async Task CleanUp()
		{
			Session.iId = -1;
			await js.RemoveItem(TOKENKEY);
			await js.RemoveItem(EXPIRATIONTOKENKEY);
			httpClient.DefaultRequestHeaders.Authorization = null;
		}
	}
}
