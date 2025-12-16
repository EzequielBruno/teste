# VR PPE Training para Mineração (Quest 2)

Este repositório contém um esqueleto de projeto Unity para um treinamento básico de EPI em ambiente de mineração direcionado ao Óculos Quest 2.

## Conteúdo
- `UnityProject/README.md`: guia rápido de configuração e build.
- `UnityProject/Assets/Documentation/PPETrainingDesign.md`: design da cena, fluxo e integrações com XR Interaction Toolkit.
- `UnityProject/Assets/Scripts/PPE/`: scripts C# para checklist de EPI, validação e zona de segurança.

## Como usar
1. Crie um projeto em Unity 2022.3 LTS (3D ou URP) e copie a pasta `Assets` deste repositório para seu projeto.
2. Instale XR Interaction Toolkit e configure OpenXR com suporte ao Quest 2.
3. Monte a cena seguindo o design proposto e associe os scripts aos objetos correspondentes.
4. Faça build para Android e teste no headset.
