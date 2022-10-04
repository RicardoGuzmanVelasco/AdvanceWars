# Día 5

## Diseño

### Alejandro

- No entiendo a qué se refiere con "Barracas y generalización" en la TODO-list.
- Revisar brevemente lo del `space.HealOccupant()` y `space.ReplenishOccupantAmmo()`.

### Culo

- No me termina de gustar el nombre de spawnable units, a ver si se ocurre algo mejor. Y que se repita.
- Creo que va tocando segregar la interfaz del IAllegiance como hablamos. Hay muchas cosas que se prueban si es aliado, pero queremos saber si es la misma nacion.
- Creo que el CO está creciendo demasiado en responsabilidad con el tema de spawnear unidades. Que luego será comprar unidades. Parece que la cosa tiende a crear un "Recruiter" o "DrillSargent"
- El spawn de una unidad se ha quedado regulinchi, ya que se le da la orden de esperar nada más spawnearse. Habrá que ver una manera de ver qué unidades están cansadas sin llamar a order. Para cuando se quiera volver atrás y tal.

## Retro

### Culo

- Parece que andamos perdiendo fuelle con el proyecto. O a lo mejor solo han sido un par de semanas raras. Aunque sigue para delante y es fácil ver qué ha hecho la gente.
    - Yo sigo aprendiendo un montón de montones


## Conclusiones generales

- Eliminar los visitantes de espacios cuando toquemos eventualmente eso otra vez.
- Es problematico que el comprar un batallon no sea una maniobra para el ctrl z
- No creemos que sea preocupante no cambiar todavia lo de los compatriotas y aliados. Cuando se haga los bandos.
- El desarrollador es quien tiene que cambiar la mentalidad respecto a los builders (por lo del map) en el Commanding Officer.
- El refactor de que los batallones siempre tuvieran 100 de fuerzas máximas se enmarronó por el tema de las fuerzas infinitas en los tests de captura. Se solventará un día a pantalla partida entre los 3.
- Alejandro y Culo tienen que revisar la ley de demeter