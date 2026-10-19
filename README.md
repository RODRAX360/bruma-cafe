# Bruma Café · menú en realidad aumentada

**Bruma Café** es un nombre provisional. Esta es una cafetería nueva, con paleta marrón y taza minimalista. Productos y precios son ejemplos editables. El PDF de Gatú se usó solo como inspiración general.

## Cómo funciona

La app de **Unity para Android con ARCore** abre la cámara RA. Cuando empieza el seguimiento, muestra un panel con el QR en el espacio frente al usuario. Ese QR lleva a la [carta digital](https://rodrax360.github.io/bruma-cafe/) alojada en GitHub Pages. Se puede pulsar **Centrar QR** para colocarlo de nuevo. Para abrir la carta en el mismo móvil, se pulsa **Abrir carta**; un QR mostrado en la propia pantalla requiere otro dispositivo para escanearlo.

## Abrir y compilar la app Android

1. Abre este repositorio en **Unity 2022.3** con los módulos **Android Build Support**, **SDK/NDK Tools** y **OpenJDK** instalados.
2. Espera a que Unity importe **AR Foundation 5.0.6**, **ARCore XR Plugin 5.0.6** y **XR Plug-in Management 4.3.3** del `Packages/manifest.json`.
3. En Unity, selecciona **Cafetería → Crear escena AR**. La escena `Assets/MenuAR.unity` tendrá **AR Session**, **XR Origin (Mobile AR)** y **AR Camera**, con el QR cargado como textura sin compresión.
4. Ve a **File → Build Settings**, selecciona **Android** y pulsa **Switch Platform**.
5. En **Edit → Project Settings → XR Plug-in Management → Android**, activa **ARCore**. Revisa las recomendaciones de **Project Validation**.
6. Conecta un Android compatible con ARCore, concede permiso de cámara y usa **Build And Run**.

La escena AR se crea dentro del editor porque sus objetos dependen de los paquetes XR importados. No se puede comprobar el seguimiento de ARCore en el editor sin un dispositivo Android compatible.

## Publicar la carta

El sitio web está en `docs/index.html`. En [Settings → Pages](https://github.com/RODRAX360/bruma-cafe/settings/pages), elige **Deploy from a branch**, rama **main** y carpeta **/docs**. Después comprueba que abra [https://rodrax360.github.io/bruma-cafe/](https://rodrax360.github.io/bruma-cafe/). El QR ya está generado para esa URL; actívala antes de imprimirlo.

## Illustrator y contenido

`design/afiche-illustrator.svg` es un afiche vectorial editable en Illustrator. `design/logo-taza.svg` contiene el logotipo. Para modificar el menú, edita `docs/index.html`. `data/menu.json` es una copia estructurada del contenido de muestra. Si cambia la URL de la web, ejecuta `python3 -m pip install qrcode[pil]` y luego `python3 generar_qr.py 'https://NUEVA-URL/'` antes de volver a compilar la app.

## Verificación

Se validó la estructura del proyecto, los archivos web y SVG, y se generó el QR para la URL de GitHub Pages. En este entorno no hay Unity ni un Android con ARCore, así que falta compilar y probar la app en un dispositivo real.
