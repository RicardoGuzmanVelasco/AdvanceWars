# Dia 3

## Retro

### Culo

### Ricardo

- Hay unas ObjMothers Enemy/Friend para los batallones que se podrían usar n test como MoveTactic_IsNotAvailable...
- Tema MapBuilder.
- Línea Map::78 commit 5ed6ab53.
- Tema (gran melón) de haber diseñado la maniobra de combinar unidades sesgado por la vista.

- ¡Cuidado! He intentado reajustar los namespaces tras introducir carpeta Domain y el IsExternalInit se pierde de los builders.
  - Es una restricción técnica por bug de Unity por lo que no pierdo ni un segundo en ello. Se quedan sin ajustar.

- Interesante la descripción del commit 8357b6c7.

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
  - Esto ha llevado a la rotura de dos test que utilizan algo sin sentido invariante: batallón con fuerzas infinitas.
    - Intenté arreglarlo con un battalion stub builder pero no funcionaba bien porque requería muchos detalles internos.
    - Para poder hacerlo hay que elevar la abstracción de esos test para poder aplicar dos veces la maniobra.
      - Surge una conversación que debemos tener: tensión entre invariantes y situaciones hipotéticas en test.

### Alejandro

- Me parece muy interesante la separación que ha hecho Ricardo de que por un lado va el CursorRendering y por otro el CursorMovement, ¿sería partir un mismo caso de uso (mover cursor) en diferentes responsabilidades? (CursorMovement sería de entrada y CursorRenderin de salida)
- He mirado la wiki y no existe la "unidad que cura otras unidades".
- He ido a clampear Battalion.Forces pero me han fallado tests en los que aparentemente no se utilizan datos realistas: ¿no es siempre Battalion.MaxForces == 100? No he querido abordar esta cuestión.