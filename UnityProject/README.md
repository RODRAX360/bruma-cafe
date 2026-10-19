# Bruma Café RA · proyecto Unity

Abre **esta carpeta `UnityProject`** en Unity Hub con Unity 2023.1.22f1 y Android Build Support instalado.

1. Espera la importación de AR Foundation, ARCore y XR Plug-in Management.
2. Abre `Assets/MenuAR.unity`; ya contiene AR Session, XR Origin, AR Camera y el panel QR. Si necesitas reconstruirla, selecciona **Cafetería → Crear escena AR**.
3. En Build Settings, cambia la plataforma a Android.
4. En Project Settings → XR Plug-in Management → Android, activa ARCore y revisa Project Validation.
5. Usa Build And Run en un Android compatible con ARCore.

El QR aparece como panel en la vista RA cuando empieza el seguimiento. **Abrir carta** abre el menú web en el mismo teléfono. La URL actual está en `Assets/Resources/menu-url.txt`.
