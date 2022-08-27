# Día 1

## Retro

### Ricardo

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

- Hay una charla rica a este respecto: si se usa intensivamente diseño por contrato ¿pierde fuerza el NullObjectPattern?
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