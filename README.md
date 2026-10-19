# Bruma Café · carta PDF con QR

El QR abre directamente [esta carta PDF](https://rodrax360.github.io/bruma-cafe/carta.pdf), que corresponde al archivo [`docs/carta.pdf`](docs/carta.pdf) señalado para el proyecto. La dirección principal de GitHub Pages redirige al mismo PDF.

El QR está en `design/menu-qr.png` y `design/menu-qr.svg`. El afiche editable en Illustrator está en `design/afiche-illustrator.svg`. Para cambiar el destino, ejecuta `python3 generar_qr.py 'https://NUEVA-URL/carta.pdf'` después de instalar `qrcode[pil]`.

El proyecto Unity sigue incluido, con el mismo QR y enlace directo al PDF. Para escanear el QR con un celular, no es necesario abrir Unity.
