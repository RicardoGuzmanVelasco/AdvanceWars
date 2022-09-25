# Día 4

## Culo

Duración: 3H

### Intenciones

- [X]  Disponibilidad de tactica de mergear unidades
  - [X]  Una unidad no debe de poder moverse si no tiene huecos
- [X]  Mergear unidades si da tiempo

### Conclusiones

- El test de que una unidad dañada puede moverse a otra unidad de su mismo tipo, funcionó inesperadamente. Faltaba funcionalidad referente a Tactic.Move
- No me disgusta cómo ha quedado el tema de los invitados, eventualmente la responsabilidad de: puede ser invitado? Acabará en el battalion probablemente
- Ha dado tiempo. Esta sesión ha sido muy satisfactoria, apetecía un cambio

## Ricardo

Duración: ~2h.

### Intenciones
- [x] Caso de uso mover cursor.
  - [x] Documentación.
- [ ] Rango máximo explícito (se habló en la retro).
- [x] Eliminar métodos que han quedado innecesarios tras la sesión de Culo.

### Conclusiones
- Lo del rango explícito cuando me he querido poner no tenía claro qué es lo que se dijo. 
- He hecho también el flujo de mostrar y esconder cursor, que no es un caso de uso pero será usado en varios de ellos.

## Alejandro

Duración: 2'5h

### Intenciones

- [X] Arreglar test en rojo de CursorControlTests tras la sesión de Ricardo. 
- [X] Jugar al Advance Wars (GBA) para solventar lagunas de conocimiento.
- [X] Dado un Batallón con 10 Fuerzas, cuando pasa el turno en una ciudad aliada, entonces tiene 30 Fuerzas.
  - [X] Ver cuales serían los valores correctos según Advance Wars (GBA).

### Conclusiones

- Me ha parecido muy curioso que me fallara un test de la sesión de Ricardo al lanzar los tests en mi portatil en vez del en sobremesa. No le daré más importancia porque lo utilizo de manera puntual.
  - Pues parece ser que si que falla. No me fijé en concreto si cuando relancé las primeras veces en mi sobremesa, el test efectivamente pasaba o quedaba como inconclusive: simplemente ví que todos los tests estaban en verde.
- Me ha costado un buen rato de debugging el hecho de que el builder del CO asigne un Map por defecto; si no lo hiciera, me habría petado a la primera ejecución y me habría dado cuenta que no le estaba pasando el Map al CO.
- He ido a clampear Battalion.Forces pero me han fallado tests en los que aparentemente no se utilizan datos realistas: ¿no es siempre Battalion.MaxForces == 100? No he querido abordar esta cuestión.
- He confundido Battalion.Forces con Platoons, en el sentido que pensaba que eran las Forces lo que se veía en la UI.