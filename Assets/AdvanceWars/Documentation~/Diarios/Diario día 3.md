# Día 3

## Alejandro

Duración: 3'5h

### Intenciones

- [X] Separar las retros por días, como los diarios.
- [X] Un Combate entre Unidades aereas tiene lugar en el aire, independientemente del Terreno de los Espacios.
- [X] Terminar el RangeOfBuilder.
- [ ] Dado un Batallón con 1 Fuerza, cuando Batallón de Unidad curativa le cura 1, entonces tiene 2 Fuerzas.

### Conclusiones

- Se hace raro que al Teatro de Operaciones se le pase un Battlefield que luego cambie si se trata de Unidades Aereas.

## Culo

Duración: 3H

### Intenciones

- [X] Arreglar caso especial Unbesiegable
- [X] Movimiento con coste de terrenos.

### Conclusiones

- Vaya maravilla tener una cobertura de tests alta que nos dice si algo ha petado al refactorizar
- El movimiento con coste se ha quedado sin refactorizar y da mucho asco. Se me ha embarrado un refactor y he tardado. Qué asco.

## Ricardo

### Intenciones

No tengo nada claro qué hacer porque aunque haya seguido los commits me cuesta aterrizar la situación.
Por tanto, creo que simplemente voy a ir pasando por todo el código, añadiendo precondiciones aquí y allá si veo algo, etc.
Una vez dé esa primera vuelta, actualizaré esto con lo que haya apuntado que puedo hacer.

- [x] Minimizar precondiciones de rango de disparo como hablamos en la retro.
  - Sigue sobrevolando el concepto de rango pero creo que de momento no merece la pena extraerlo.
- [x] Esconder unbesiegable en lugar de darle visibilidad de paquete.
- [x] Concepto de armada para que IsAerial sea lógica de armada y no de propulsión.

### Conclusiones

- Al quitar la precondición duplicada en range of fire he visto que había ahí mucha lógica extraña.
  - Había un IsValid que no tenía sentido porque nunca podía pasar eso según las invariantes.
  - Este IsValid se comprobaba en el mapa como precondición, pero esto es un error de reparto de responsabilidades:
    - Que sea válido en general un objeto tiene que ser responsabilidad (invariante) de ese objeto.
  - Así que lo he quitado. Lo dejo aquí para que quien lo vea comente luego algo.

- Armada es un concepto menos general de lo que lo estamos usando. Es fuerzas armadas lo que decíamos.

- Me he dado cuenta de que no consideramos la creación por defecto de structs.
  - Sus strings son nulas, no vacías.
  - He refactorizado lo necesario.

- He acabado haciendo mucho más que en las intenciones porque no sabía de primera bien qué hacer.