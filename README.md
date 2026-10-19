# Bruma Café · carta PDF en realidad aumentada

La carta es el PDF [`docs/carta.pdf`](docs/carta.pdf), basado en la carta de Gatú proporcionada como ejemplo. Se conserva su disposición y contenido y se reemplazan los logos visibles por el de Bruma Café. Algunos nombres y descripciones de productos aún mencionan Gatú porque el cambio solicitado fue únicamente de logo.

## Enlace y QR

El código QR abre directamente [la carta PDF publicada](https://rodrax360.github.io/bruma-cafe/carta.pdf). `docs/index.html` redirige a ese mismo PDF. El afiche editable en Illustrator está en `design/afiche-illustrator.svg`; el QR vectorial, en `design/menu-qr.svg`. Para regenerar los QR al cambiar la URL: `python3 generar_qr.py 'https://NUEVA-URL/carta.pdf'` (requiere `qrcode[pil]`).

## Unity para Android

Abre la carpeta `UnityProject` en Unity 2023.1.22f1. La escena `Assets/MenuAR.unity` contiene **AR Session**, **XR Origin (Mobile AR)**, **AR Camera** y el panel QR. La textura del QR y el enlace directo al PDF están en `Assets/Resources/`. En Android, activa ARCore en XR Plug-in Management y compila con Android Build Support. En el teléfono, **Abrir carta** abre el PDF; para escanear el QR visible en la propia pantalla se necesita otro dispositivo.
