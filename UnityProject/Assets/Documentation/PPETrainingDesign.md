# Design do Treinamento de EPI para Mineração (Quest 2)

## Cena e layout
- **Zona de briefing**: UI com TextMeshPro, placa ou holograma mostrando objetivos e controles (gatilho para pegar, grip para soltar).
- **Bancada de EPI**: capacete, óculos, colete, luvas, botas e respirador. Cada item tem `XR Grab Interactable` e collider.
- **Zona de trabalho (SafetyZone)**: área delimitada com luz/porta holográfica que libera passagem quando todos os EPI estão equipados.

## Lógica geral
1. `PPETrainingManager` registra todos os `PPEItem` presentes na cena.
2. Cada `PPEItem` dispara evento quando é vestido (ex.: encaixado no socket da cabeça/mão/torso/pés) ou retirado.
3. A UI mostra a próxima peça a ser vestida e um checklist com status de cada item.
4. `SafetyZone` só permite entrada quando `PPETrainingManager.AllItemsEquipped == true`. Caso contrário, mostra aviso e reproduz feedback sonoro/visual.

## Integração com XR Interaction Toolkit
- Use `XRGrabInteractable` nos objetos de EPI; adicione o script `PPEItem` no mesmo GameObject.
- Para vestir, use **XR Socket Interactors** nos pontos do avatar (cabeça, mãos, torso, pés). Configure `socketActive` verdadeiro.
- Conecte os eventos `XRGrabInteractable.selectEntered` e `selectExited` aos métodos `PPEItem.OnSelected` e `PPEItem.OnDeselected` via código ou Unity Events.

## UI
- Prefab com painel de checklist (TextMeshProUGUI) e texto de instrução.
- Campo de texto de feedback (verde quando completo, amarelo/vermelho quando faltando EPI).

## Áudio e feedback visual
- Opcional: áudio curto ao completar checklist; alarme/voz ao tentar entrar sem EPI.
- Materiais emissivos ou shader de "campo de força" para a barreira da zona de trabalho.

## Métricas
- Tempo total para completar checklist.
- Número de erros (tentativas de entrar sem EPI ou remoções durante a tarefa).
- Exportar log simples em JSON (PlayerPrefs) ou enviar para servidor/LMS (HTTP POST) se necessário.

## Scripts
Veja `Assets/Scripts/PPE` para a implementação base.
