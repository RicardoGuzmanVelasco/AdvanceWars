# Dia 2

## Retro

### Alejandro

- Hay cosas en las conclusiones del Día 2 de Culo que creo deben ir en su retro: son cosas a comentar de ambito general, proceso o guía de estilo.
- Utilizar '-' en vez de '*', para ser homogéneos.
- Siempre dudo en si poner en los mensajes del commit en qué clases hago los cambios: por un lado, ya aparecen en los cambios; por otro, hay que entrar al commit para mirarlo.

### Culo

- Dividir la retro en archivos de días como el diario.
- Un refactor en un test es un refactor o es test dentro de conventional commits.
- Habria que mantener un estandar en los tests al crear cosas con string. Hay desde: "A", "aNation" "SameNation" "sameNation" "any" "ally" "Ally"...
- Me ha gustado mucho una cosa que he hecho porque no lo hacía en mi curro que es no subir los commits porque no había una cobertura suficiente, no estaba terminado y creo que iba a hacer más mal que bien. Y quedarme el insight

## Diseño

### Alejandro
- ¿Al final cuál fue la conclusión de que Space tuviera todo lo relacionado con el Asedio? 
- Teniendo que FireManeuver recibe el target y que el Map indica para un Batallón quién está en su Rango de Disparo, _Seleccionar a qué enemigo atacar_ sería responsabilidad del controlador, ¿no? ¿queremos montar los controladores?
- Para el caso de uso "Final de turno", se supone que hay que ceder el control tras lanzar el evento de "nuevo turno", pero simplemente se lanza un evento y se continua con "empieza tu turno" del siguiente CO. ¿No debería ser hacerse un yield o similar?
- ¿El final de turno realmente lo indica el Input/Vista? Creo que sería algo del Control, automático, cuando ya no queden Batallones del CO actual con Tácticas disponibles.

## Conclusiones generales

- Revisar el special case de Building.Unbesiegable y su uso en el space.
- Se separán las retros en días como los diarios.
- Hemos movido las conclusiones generales a la retro.
- Se ha decidido usar - en el .md en vez de *. Cambiarlos cuando se vean. También usar X mayúscula en los checks.
- Seguir estandar de casing (aNation, sameNation...) de la nomeclatura de los tests en las strings.
- Seleccionar a qué enemigo atacar es responsabilidad del controlador. Sí queremos montar estos controladores.
- Queremos montar la UI eventualmente.
- Dejar de usar sinónimos como nombres de variables cuando no aporten información nueva, aunque sea el mismo nombre del tipo. Como troops - battalion. Añaden ruido.
