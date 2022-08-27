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

# Día 1

## Ricardo

### Intenciones

- [X]  Lo primero, voy a ordenar la lista de cosas por hacer, porque veo cosas duplicadas y fuera de sitio.
- [x]  Voy a crear un documento con dos propósitos:
  - almacenar aquello que cada persona quiera aportar a la retro;
  - almacenar aquello que cada persona quiera que se discuta en las reuniones de diseño;
- [ ]  Quiero revisar el código nuevo a partir de su test hasta saber si debo o no añadir más casos de testing.
- [x]  Antes de ello me gustaría reducir inventario de refactoring.
- [ ]  Trataré de revisar la documentación y acercarla al código.

### Conclusiones

- He dudado si funcionaría la precondición del ctor de TheaterOps con Terrain.
  - He escrito un test de regresión de prueba y sin embargo no saltaba el rojo.
  - Se debía a haber usado el flightweight para el Terrain.Null, como decía Fowler.
  - ¡Ole!
- Me he planteado si, en el constructor de Offensive, no será mejor relegar al cliente que el atacante no sea nulo.
  - Eso facilitaba el contrato, que quedaría con dos precondiciones claras.
  - Sin embargo parece una API muy straightforward tal como está (null no hace daño), así que lo he dejado así. 