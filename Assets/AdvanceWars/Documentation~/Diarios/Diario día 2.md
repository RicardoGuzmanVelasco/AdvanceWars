# Día 2

## Culo

Duración: 3h.

### Intenciones

- [X]  Un batallon no puede atacar si no tiene arma.
  - [X]  Un batallon no puede atacar si no puede dañar a un enemigo
- [X]  Renombrar test de CanFire_AfterMove
- [X]  Precondiciones de comparables.
- [X]  Precondicion de available tactics of si es batallon enemigo
- [X]  Clase rango de disparo
- [ ]  Coste de movimiento por terreno. *NUEVO*
  - [ ]  Refactor: reusar rango de fuego y disparo
- [X]  No exponer unit en battalion
- [ ]  Arreglar caso especial de Unbesiegable

### Conclusiones

- [X]  Tambien se han cambiado los tipos allegiance a battalion en los parámetros de los métodos del commanding officer
- [X]  Hay que requerir que el min range sea positivo
- Habria que mejorar los tests de range of fire que no se entienden
- No me gusta mucho lo que he hecho con el range of fire del Valid y tal para comprobar la precondicion fuera de que no se haya creado por constructor vacio porque es repetir. Como se haria?
- Hay que renombrar RangeOfFire para que no se llame igual la clase, la property y el metodo del mapa.
- Para eliminar la duplicidad del rango de fuego y el rango de disparo, creo que es una mejor decision primero hacer el tema de costes de movimiento, luego saldrá mucho más fácil
- Tenemos 2 metodos Range of movement, uno le pasamos un batallon y busca el espacio y llama al otro. El otro coge el espacio y pilla el batallon.
- No me ha dado tiempo a arreglar el caso especial de Unbesiegable. También no creo que lo entienda del todo. Es solo un caso que se deberia usar cuando se intenta asediar un edificio aliado?

## Alejandro

Duración: 3h

### Intenciones

- [x] Arreglar caso especial de Unbesiegable.
- [x] Terreno aire para los combates (0 de defensa).
- [ ] Seleccionar a qué enemigo atacar.
- [ ] TODO en OperationTests: "When turn starts, it starts Commanding Officer Turn. Implementation test".
- [ ] Revisar RangeOfFireTests para mejorar legibilidad (conclusiones de Culo).

### Conclusiones
- Ya se tiene Terreno Aire para los combates, ¿siguiente paso al respecto? ¿borrar de la TODO-list?
- Teniendo que FireManeuver recibe el target y que el Map indica para un Batallón quién está en su Rango de Disparo, _Seleccionar a qué enemigo atacar_ sería responsabilidad del controlador, ¿no? ¿queremos montar los controladores?
- TODO en OperationTests: Lo he movido a TODO-list; es más interesante cuando estén dichas Maniobras automáticas.
- He decidido abordar el MapBuilder que permite construir un Map desde una string formateada (verbatim, en este caso). Lo he empezado, pero falta la gran mayoría de casuística.