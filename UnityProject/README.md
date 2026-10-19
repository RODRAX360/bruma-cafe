# Bruma Café RA · proyecto Unity

Abre **esta carpeta `UnityProject`** en Unity Hub con Unity 2022.3 y Android Build Support instalado.

1. Espera la importación de AR Foundation, ARCore y XR Plug-in Management.
2. Selecciona **Cafetería → Crear escena AR**. Esto crea `Assets/MenuAR.unity` con AR Session, XR Origin y AR Camera.
3. En Build Settings, cambia la plataforma a Android.
4. En Project Settings → XR Plug-in Management → Android, activa ARCore y revisa Project Validation.
5. Usa Build And Run en un Android compatible con ARCore.

El QR aparece como panel en la vista RA cuando empieza el seguimiento. **Abrir carta** abre el menú web en el mismo teléfono. La URL actual está en `Assets/Resources/menu-url.txt`.
