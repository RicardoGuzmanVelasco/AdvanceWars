# Día 0

## Alejandro y Culo

### Intenciones

- [X]  Crear maniobra de asedio.
- [X]  Digitalizar TO-DO list no digitalizada hasta ahora.

### Conclusiones

- Ahora al llamar al AvailableTacticsOf de un batallón, devuelve empty si el batallón no es aliado
- Se ha añadido una precondición en el Order del CommandingOfficer para que no deje ordenar maniobras con Performer no aliados.
- Se han añadido varias precondiciones a los spaces.

- En el futuro tendremos problemas con la diferenciación de Ally y Self al tener los bandos.
- (Alejandro) Uso de Null pattern únicamente por conveniencia de los tests es raro: Battalion, Map, Space, etc.
- Clases parciales mezcladas con internas resulta confuso debido a los NullObjectPattern: Map y Space.
- Duda: ¿Mover clases de directorio es una chore o refactoring?
- Duplicación WhereIs(Batallion) en las maniobras. ¿Usar Space como parámetro en vez de un performer?
- Habría que definir con calma la estructura del diario.