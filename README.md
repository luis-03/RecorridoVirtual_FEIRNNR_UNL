# Recorrido Virtual Inmersivo de los Exteriores de la FEIRNNR – UNL

Este proyecto consiste en el desarrollo de un **recorrido virtual fotorrealista e inmersivo** de los exteriores de la Facultad de la Energía, las Industrias y los Recursos Naturales No Renovables (FEIRNNR) de la **Universidad Nacional de Loja**. El sistema utiliza **fotogrametría híbrida** para la reconstrucción tridimensional y está optimizado para su ejecución en dispositivos de Realidad Virtual (VR).

---

## 📋 Información del Proyecto
*   **Autor:** Luis Miguel Jiménez Morocho
*   **Director:** Ing. Andrés Roberto Navas Castellanos
*   **Institución:** Universidad Nacional de Loja (UNL)
*   **Facultad:** FEIRNNR
*   **Carrera:** Computación
*   **Año:** 2026

---

## 🛠️ Tecnologías y Herramientas
El proyecto integra diversas tecnologías para lograr un equilibrio entre fidelidad visual y rendimiento:

*   **Motor Gráfico:** Unity 6000.0.59f2 (URP - Universal Render Pipeline).
*   **Fotogrametría:** Agisoft Metashape Professional (Procesamiento de capturas aéreas y terrestres).
*   **Modelado y Optimización:** Blender (Retopología manual, Bakeado de texturas y LODs).
*   **Hardware de Captura:** Dron DJI Mavic 3 Enterprise y Cámara Sony ZV-E10 II.
*   **Dispositivo Objetivo:** Meta Quest 2 (Realidad Virtual Autónoma).
*   **Librerías XR:** OpenXR & XR Interaction Toolkit.

---

## 🚀 Características Principales
*   **Navegación Libre:** Exploración en primera persona por los exteriores de la facultad.
*   **Fotorrealismo:** Modelos 3D generados a partir de fotografías reales mediante fotogrametría híbrida.
*   **Optimización Avanzada:** 
    *   Sistemas de **LOD (Level of Detail)** para vegetación y edificios.
    *   **GPU Instancing** para el renderizado eficiente de áreas verdes.
    *   **Baking de iluminación** y texturas para alto rendimiento en dispositivos móviles (Quest).
*   **Locomoción VR:** Soporte para movimiento continuo y teletransporte.

---

## 📁 Estructura del Repositorio
Debido al peso de los activos de fotogrametría, el proyecto utiliza **Git LFS**.
*   `/Assets`: Scripts, materiales, modelos 3D y escenas de Unity.
*   `/ProjectSettings`: Configuraciones globales del proyecto.
*   `/Packages`: Dependencias y paquetes de Unity.

---

## 🔧 Instrucciones de Instalación
1.  **Clonar el repositorio:**
    ```bash
    git clone [https://github.com/luis-03/RecorridoVirtual_FEIRNNR_UNL.git](https://github.com/luis-03/RecorridoVirtual_FEIRNNR_UNL.git)
    ```
2.  **Instalar Git LFS** (necesario para descargar los modelos 3D pesados):
    ```bash
    git lfs install
    git lfs pull
    ```
3.  **Abrir con Unity:** Utilizar la versión **6000.0.59f2** o superior para asegurar compatibilidad con los prefabs de XR.

---

## 📊 Evaluación de Presencia
La experiencia inmersiva fue evaluada utilizando el instrumento **Igroup Presence Questionnaire (IPQ)**, obteniendo resultados positivos en las dimensiones de:
1.  Presencia General
2.  Presencia Espacial
3.  Involucramiento
4.  Realismo Experimentado

---

## ⚖️ Licencia y Autoría
Este trabajo es el resultado de un **Trabajo de Integración Curricular** previo a la obtención del título de Ingeniero en Ciencias de la Computación. Todos los derechos reservados por la Universidad Nacional de Loja y el autor.
