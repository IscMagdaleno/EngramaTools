


window.descargarArchivo = (base64, nombre, tipo) => {
    const link = document.createElement('a');
    link.download = nombre;
    link.href = `data:${tipo};base64,${base64}`;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}