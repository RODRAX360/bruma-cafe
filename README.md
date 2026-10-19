# Bruma Café · carta web y proyecto Unity

**Bruma Café** es un nombre provisional para la nueva cafetería; puede cambiarse por la marca definitiva.

Cafetería nueva de muestra. El PDF proporcionado sirvió solo como referencia general de cafetería; el menú, la marca y el afiche de este proyecto son nuevos. Productos y precios son ejemplos editables.

## Publicar la carta en GitHub Pages

1. Crea un repositorio público de GitHub y sube **todo el contenido de esta carpeta** a la raíz del repositorio.
2. En el repositorio, abre **Settings → Pages**. En **Build and deployment**, selecciona **Deploy from a branch**, rama `main` y carpeta `/docs`. Guarda los cambios.
3. Espera la URL pública, normalmente `https://USUARIO.github.io/REPOSITORIO/`. Comprueba que abre `docs/index.html`.
4. El QR ya está generado para `https://rodrax360.github.io/bruma-cafe/`. Si cambia el nombre del repositorio o la URL pública, ejecuta `python3 -m pip install qrcode[pil]` y luego `python3 generar_qr.py 'https://NUEVA-URL/'`.
5. El QR está incluido en `design`, `docs` y `Assets/Resources`. Comprueba que GitHub Pages esté activo antes de imprimirlo.

La página del repositorio (`github.com/USUARIO/REPOSITORIO`) muestra archivos. La URL de GitHub Pages (`USUARIO.github.io/REPOSITORIO`) abre la carta directamente; esta es la URL recomendada para el QR.

## Unity

Abre la carpeta en Unity Hub con Unity 2022.3. En el editor, selecciona **Cafetería → Crear escena del menú**. Pulsa Play para revisar la carta. Para una versión Unity WebGL, cambia la plataforma a WebGL en Build Settings y compila. El botón de Unity abre la misma URL de la carta cuando el QR está configurado.

## Illustrator

Abre `design/afiche-illustrator.svg` en Illustrator. Contiene el QR completo en vectores. El diseño y el QR siguen siendo editables.

## Editar contenido

Modifica `docs/index.html` para la carta web y `Assets/Resources/menu.txt` para Unity. `data/menu.json` conserva una copia estructurada de la carta de muestra. Ajusta también nombre, colores y textos del sitio, el script de Unity y el afiche según la marca final.

## Verificación

Se revisaron estructura, HTML, JSON y SVG. Unity e Illustrator no están instalados en este entorno, por lo que no se pudo ejecutar el proyecto ni abrir el afiche en esas aplicaciones.
