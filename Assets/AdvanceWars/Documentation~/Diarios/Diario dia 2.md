# Día 0

## Alejandro y Culo

### Intenciones

- [x] Un batallon no puede atacar si no tiene arma.
    - [x] Un batallon no puede atacar si no puede dañar a un enemigo
- [x] Renombrar test de CanFire_AfterMove
- [x] Precondiciones de comparables.
- [x] Precondicion de available tactics of si es batallon enemigo
- [ ] Clase rango de disparo
- [ ] Refactor: reusar rango de fuego y disparo
- [x] No exponer unit en battalion
- [ ] Arreglar caso especial de Unbesiegable

### Conclusiones

- [x] Tambien se han cambiado los tipos allegiance a battalion en los parámetros de los métodos del commanding officer
- [x] Hay que requerir que el min range sea positivo
- Habria que mantener un estandar en los tests al crear cosas con string. Hay desde: "A", "aNation" "SameNation" "sameNation" "any" "ally" "Ally"...
- En algunos sitios usamos troops en vez de battalion. Hasta que punto no estamos usando sinonimos que no aportan nada frente al lenguaje ubicuo.