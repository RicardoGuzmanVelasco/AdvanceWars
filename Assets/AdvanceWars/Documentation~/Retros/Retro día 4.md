# Dia 3

## Retro

### Culo

### Ricardo
- Hay unas ObjMothers Enemy/Friend para los batallones que se podrían usar n test como MoveTactic_IsNotAvailable...
- Tema MapBuilder.
- Línea Map::78 commit 5ed6ab53.
- Tema (gran melón) de haber diseñado la maniobra de combinar unidades sesgado por la vista.

## Diseño

### Culo

- Tocar cualquier cosa del mapa con movimiento es bastante farragoso sin el refactor.
- Si un batallon solo puede moverse de un Space A a un Space B, siendo B un espacio ocupado por otro batallón en el que el primero puede mergearse:
    - He decidido que el batallón se podrá mover, y cuando esté en el otro sitio, podrá hacer la otra acción o volver para atrás, me parecía farragoso lo otro.
- Join vs Merge. En el advance wars usan Join, pero me parece bastante mejor usar Merge.
- El set del forces creo que debería de ser privado, no se debería de clampear a MaxForces y a 0. Podemos hacer Damage y Heal
- Habria que mover responsabilidad del MergeManeuver al Space, como se hizo con el fire?

### Ricardo

- SetForces puede clampear si se usa backing field en vez de autoproperty.
  - En C# 11 esto ya no es necesario porque la autoproperty puede tener operaciones de get/set personalizadas con lambda.