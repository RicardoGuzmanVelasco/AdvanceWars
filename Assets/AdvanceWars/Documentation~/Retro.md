# Día 1

## Retro

### Ricardo

Trataré de leer todo muy rápido de inicio y luego ahondar en cada cosa.
Si no, por experiencia en el trabajo, se consume menos tiempo cuanto más se avanza en los temas.

- El proceso tiene un vacío. Propongo: cuando el último del ciclo termina, debería avisar al resto antes de que se haga la retro, para que estos tengan opción a leer cambios que no han visto y añadir lo que consideren a la retro.
  - Esto lo añadiré a Epistolary Programming Explained (Second Edition), no preocuparse.
- Propongo repasar toda la lista de TODO para borrar aquello que haya quedado viejo o poco plausible (para eso está el control de versiones).
- No creo que la documentación técnica haya que actualizarla exhaustivamente. Si acaso, de vez en cuando tras grandes features.
  - ¿Hacemos una carpeta solo para los diagramas, subcarpeta de documentación?
- El formato [ ] en las intenciones del diario me ha dado curiosidad.
  - De inicio no me ha gustado, porque es más peñazo.
  - Después me ha sido útil porque me sirve para ir haciéndome de autobitácora, más que otra cosa.
- Creo que se está poblando sin suficiente cuidado la lista de cosas por hacer. Ejemplos:
  - Líneas duplicadas en varias categorías.
  - Líneas duplicadas respecto a otros documentos.
  - Líneas fuera de categoría.
- Discusión propuesta respecto a convenir el formato del diario.
  - Yo creo que está bien como está y que no debe usarse para mucho más que eso. Por evitar burocracia que no queremos.
- Me resulta curioso cómo pueden diferir las estrategias del NullObjectPattern.Por ejemplo:
  - en clases y records a veces se crea caso especial hijo y otras (Unit, Terrain, v. g.) se construye en factory method con valores por defecto sin más.
  - en structs como MovRate o Propulsion lo hemos hecho con valores distintos ya que una es un int = 0 (su default) y otra con string = empty (su default sería el nulo).
- A propósito del tema NullObjectPattern tras la sesión de Alejando:
  - Se ha usado de una manera que, además de (en mi opinión) no ser correcta, difiere del uso que se ha dado en el resto de lugares.
    - Se trata de mantener público el caso especial Unbesiegable y sobreescribir su igualdad.
    - En lugar de no exponer esa clase y hacer flightweight su factory method a través de la clase que la contiene.
- Al respecto del problema para separar entre aliado y propio,
  - creo que es una muy buena puntualización;
  - sin embargo, como se dice, creo que es algo a obviar salvo, y solo salvo, por la mecánica de los "bandos".
- La confianza al tirar los test de que no he roto nada vuestro que desconozco (o nuestro anterior que sí conozco) es EXTREMA. ¡¡¡!!!
- Hay una charla rica: si se usa intensivamente diseño por contrato ¿pierde fuerza el NullObjectPattern?
- Creo que la idea de representar el tablero mediante comentarios es muy buena pero ¡se puede aprovechar pa hacer un builder con ella!
  - Es algo que Luis hizo en el tic-tac-toe y me reventó la cabeza, me parece genial.
  - En C# se haría con las verbatim strings estas que saltan de línea, @"".
- Creo que en las tácticas no debería ir la decisión sobre cuándo una táctica es potencialmente disponible o no.
  - Porque la táctica tiene naturaleza de tipo y además va a tener que recibir muchos datos no-por-constructor.
  - Porque va a dificultar bastante el uso de las tácticas, imho.
  - Sí que me parece buena idea sacarlo a algún sitio X.
- El uso de uint para marcar invariante de no negativo me pareció una buena idea en su momento pero lo acabé descartando.
  - Incluso Microsoft, con sus dos..., lo mencionaba en un artículo de su blog. Pero no sé dónde lo vi.
  - Decían que el convenio tácito es tan de usar int sin más, que lo desaconsejaban para invariantes y lo dejaban solo para una cuestión de rendimiento, red y demás.
  - Además, en Unity en concreto se da esta problemática del convenio tácito porque esas clases ni serializan entonces vas a tener que estar haciendo moldes desde int y tal todo el rato.
  - Cuando yo lo intenté (en el trabajo) fue por eso por lo que lo acabé descartando. Incomodas con esa API porque la haces aparatosa para el cliente.
- Existen precondiciones de comparables (mayor que, menor o igual...).
- Diferencia entre retro y conclusiones:
  - Pretendía que las conclusiones fueran un comentario a las propias intenciones de uno mismo al empezar la sesión.
  - A la retro van cosas más generales para mejorar el proceso o el conocimiento tribal.
- Cuestión de dominio: ¿no es el rango de disparo, como el de movimiento, igual en las dos dimensiones?
  -Respuesta: no son dos dimensiones lo que hay ahora mismo en rango de disparo sino rango mínimo y máximo.

### Culo

## Diseño

### Ricardo

- Sobre la duplicidad de WhereIs(battalion) en CO.TacticsOf y en las maniobras,
  - no creo que haya que preocuparse por el momento, en tanto que el código que lo usa queda muy muy legible y, además, la API queda más natural imho que si se cambia a espacio;
  - quizá podemos verlo de nuevo más adelante.

- En mi opinión, devolver Empty en las tácticas disponibles de batallón no aliado es:
  1. Programación defensiva (no la queremos en domino ¿no?).
  2. Ambiguo: ¿cómo se diferencia respecto al hecho de un batallón que realmente no tiene tácticas disponibles?

- Se puede en el futuro segregar el concepto "nacional" (INational) que proponía Culo en su momento.
  - Sería una segregación de Allegiance.
  - Contendría, solamente, la lectura de la nación (una read-only property).
  - Las maniobras la implementarían.
    - De esta manera, quizá se podrían evitar accesos no necesarios al batallón de una maniobra.
  - Con lo anterior, lo que propongo es revisarlo más adelante para ver si hace realmente esa labor. Ahora mismo creo que no la haría.

- (copiado de TODO list): Renombrar Building por Property, respetando el diagrama.
  - ¿Y si lo que se cambia es el diagrama y la noción de "tipo de edificio"?
  - A fin de cuentas, property fue un término que introdujimos nosotros. A la vista está que no cuajó en el imaginario del equipo.
- Creo que Origin como rol de la táctica en la maniobra es un nombre desacertado. Todos nos hemos equivocado ya en diferentes ocasiones al ir al usarlo.
- No entiendo qué son newCoordinates en un mapa (quiero indirectamente decir que debemos mejorar el nombre).
- Sacar métodos como CoordsInsideRange de mapa (son extensiones matemáticas puras).
- MinMaxRange en el ataque de la unidad es un intervalo.
- Precondición al rango de fuego de un batallón por si no está en el mapa ¿no?

### Alejandro

- ¿Leer la correspondencia que recibes entra dentro de la Duración?

### Culo

- Habría que ir pensando en crear assembly definitions y en como dividirlas, estamos manteniendo la disciplina pero ahora que tenemos menos contacto, es más propicio a que se rompa eso. Además cada vez es más complicado encontrar las cosas.
- Habría que mover los tests del mapa al una clase tests de mapa
- No entiendo qué va en la parte de la retro y qué parte en conclusiones, no es lo mismo?
- Refactor: Duplicidad entre RangeOfFire y RangeOfMovement?
- MinRange - MaxRange vs RangeeOfFire
- Refactor: Crear Range con min y max value para que la unit no tenga 2 enteros.
- Refactor: Borrar overload de RangeOfFire que solo tiene maxRange?
- No sé si el cambio que le he hecho al IsBesiegable es correcto. Pero no le veía el sentido a la precondición. Por qué petaba al tener un Terrain.Null? Debería de no petar, no?
- Borrar test Troops_CanFire_AfterMove ? Ya no siempre es el caso de que las tropas puedan disparar siempre despues de moverse. Se ha cambiado para estar en verde
- El rango es del arma o de la unidad? Todas las armas de una unidad tienen el mismo rango, pero hay unidades que no tienen armas y entiendo que no tienen rango.

# Dia 2

## Retro

### Alejandro

- Hay cosas en las conclusiones del Día 2 de Culo que creo deben ir en su retro: son cosas a comentar de ambito general, proceso o guía de estilo.
- Utilizar '-' en vez de '*', para ser homogéneos.
- Siempre dudo en si poner en los mensajes del commit en qué clases hago los cambios: por un lado, ya aparecen en los cambios; por otro, hay que entrar al commit para mirarlo.

## Diseño

### Alejandro
- ¿Al final cuál fue la conclusión de que Space tuviera todo lo relacionado con el Asedio? 
- Teniendo que FireManeuver recibe el target y que el Map indica para un Batallón quién está en su Rango de Disparo, _Seleccionar a qué enemigo atacar_ sería responsabilidad del controlador, ¿no? ¿queremos montar los controladores?
- Para el caso de uso "Final de turno", se supone que hay que ceder el control tras lanzar el evento de "nuevo turno", pero simplemente se lanza un evento y se continua con "empieza tu turno" del siguiente CO. ¿No debería ser hacerse un yield o similar?
- ¿El final de turno realmente lo indica el Input/Vista? Creo que sería algo del Control, automático, cuando ya no queden Batallones del CO actual con Tácticas disponibles.