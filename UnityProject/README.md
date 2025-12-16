# VR PPE Training (Meta Quest 2)

Este diretório descreve um projeto base em Unity (2022.3 LTS) para um treinamento imersivo de EPI em um ambiente de mineração, voltado ao Óculos Quest 2.

## Objetivo do treinamento
- Ensinar colocação correta de EPI (capacete, óculos, colete, luvas, botas e respirador/mascara).
- Validar checklist antes de entrar na frente de serviço.
- Orientar o usuário sobre áreas seguras e zonas bloqueadas quando o EPI não está completo.

## Requisitos principais
- Unity 2022.3 LTS.
- XR Interaction Toolkit 2.5+.
- Meta XR All-in-One / OpenXR para Quest 2 (Android build target).
- TextMeshPro para UI.

## Estrutura sugerida
```
UnityProject/
  Assets/
    Documentation/
      PPETrainingDesign.md
    Scripts/
      PPE/
        PPEItem.cs
        PPETrainingManager.cs
        SafetyZone.cs
```

## Configuração rápida
1. Crie um projeto 3D (URP opcional) no Unity 2022.3 LTS e substitua o conteúdo de `Assets` pelos arquivos deste diretório.
2. Instale os pacotes via Package Manager:
   - **XR Interaction Toolkit** (habilite as amostras `Starter Assets`).
   - **OpenXR** e habilite o profile **Meta Quest Support**.
   - **Meta XR All-in-One** (opcional para features específicas do Quest 2).
3. Em **Project Settings > XR Plug-in Management**, selecione OpenXR para Android.
4. Em **Project Settings > Player**, configure:
   - **Company/Product** conforme sua empresa.
   - **Minimum API Level**: Android 10 (API 29) ou superior.
   - **Target Architectures**: ARM64.
5. No **Input System**, habilite o novo Input System (se solicitado).
6. Configure a cena:
   - Adicione um **XR Origin (Action Based)**.
   - Adicione os prefabs de mão/controlador do Starter Assets.
   - Coloque os objetos de EPI (capacete, óculos, colete, luvas, botas, respirador) com componentes **XR Grab Interactable**.
   - Adicione os scripts `PPEItem`, `PPETrainingManager` e `SafetyZone` conforme descrito em `Assets/Documentation/PPETrainingDesign.md`.

## Build para Quest 2
1. Troque para **Android** em **Build Settings**.
2. Conecte o headset via Link (ou use build/side-load com oculus-usb). 
3. Faça **Build & Run**.

## Fluxo de jogo
1. O usuário inicia em uma área segura com instruções.
2. O `PPETrainingManager` mostra etapas na UI (TextMeshPro) e valida cada peça de EPI ao ser vestida.
3. Ao completar todos os itens, a zona de trabalho (`SafetyZone`) é liberada. Se remover algum item, o acesso volta a ser bloqueado e um alerta sonoro/visual é exibido.

## Próximos passos sugeridos
- Adicionar checagem de integridade (por exemplo, craqueamento de capacete).
- Registrar tempo de conclusão e erros para score.
- Incluir voz off / instruções auditivas.
- Integrar com backend de LMS (opcional).
