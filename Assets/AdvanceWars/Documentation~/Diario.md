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

Duración: ~3h.

### Intenciones

- [X]  Lo primero, voy a ordenar la lista de cosas por hacer, porque veo cosas duplicadas y fuera de sitio.
- [X]  Voy a crear un documento con dos propósitos:

- almacenar aquello que cada persona quiera aportar a la retro;
- almacenar aquello que cada persona quiera que se discuta en las reuniones de diseño;

- [X]  Quiero revisar el código nuevo a partir de su test
  - [ ]  hasta saber si debo o no añadir más casos de testing.
- [X]  Antes de ello me gustaría reducir inventario de refactoring.
- [ ]  Trataré de revisar la documentación y acercarla al código.

### Conclusiones

- He dudado si funcionaría la precondición del ctor de TheaterOps con Terrain.
  - He escrito un test de regresión de prueba y sin embargo no saltaba el rojo.
  - Se debía a haber usado el flightweight para el Terrain.Null, como decía Fowler.
  - ¡Ole!
- Me he planteado si, en el constructor de Offensive, no será mejor relegar al cliente que el atacante no sea nulo.
  - Eso facilitaba el contrato, que quedaría con dos precondiciones claras.
  - Sin embargo parece una API muy straightforward tal como está (null no hace daño), así que lo he dejado así.
- He acometido lo de que no se pueda levantar un asedio que no se inició.
  - Añadiendo la precondición directamente ha funcionado.
  - Eso me ha llevado a pensar que nos faltan test donde se use Unoccupy sin haber iniciado asedio.
  - Lo he creado y efectivamente ha fallado.
  - He resuelto el test haciendo algo que se propuso en algún momento: preguntando al Terrain si está bajo asedio.
- He dejado para el final lo de la documentación para no hacer trabajo en balde y al final no me ha dado tiempo.
- Aunque he revisado el código que se añadió, no he llegado a profundizar en sobre si añadir o no más casos de test.
  - A fin de cuentas, como vosotros veríais en el día 0, todo está delegado a otras clases y por tanto se supone que a otros test.

# Alejandro

Duración: ~3h.

### Intenciones

- [X]  Revisar `anEnemyOccupant_Leaves_WithoutStartAnySiege_OverTheBuilding`: no tiene aserciones.
- [X]  Revisar que un UnbesiegableBuilding no puede ser, efectivamente, asediable.
  - La definición de un Building como Unbesiegable de que `maxSiegePoints == int.MaxValue` no cumple con la query `IsUnderSiege => maxSiegePoints > SiegePoints;`
- [ ]  Mergear Batallones.

### Conclusiones

- Añado a `anEnemyOccupant_Leaves_WithoutStartAnySiege_OverTheBuilding` un Should().NotThrow<Exception>() para declarar intencionalidad y evitar futuras confusiones.
- La precondición sobre IsUnderSiege es redundante, ya que ya no se muestra como disponible la Tactic de Asediar. La dejo por cuestiones comunicativas.
- He mirado lo de mergear Batallones, pero en el de GBA no he visto tal mecánica; me falta conocimiento del dominio. Habría que definir esto.

# Culo

Duración:

### Intenciones

- [ ]  No exponer Unit en Batallion
- [ ]  La orden de disparar no aparece si la unidad no tiene armas.
- [ ]  Hacer rango de ataque y condición de poder realizar maniobra de ataque

  - [ ]  Preveo que va a haber mucha responsabilidad en el Commanding Officer para comprobar si una táctica es válida o no. A lo mejor ese comportamiento puede ser de la Táctica.

### Conclusiones

* No me gusta como se ha quedado un test de rango de disparo. Me gustaría ver cómo lo haríais.
*
