# Description
Desarrollo de un sistema para crear y administrar ordenes de pagos, se ha desarrollado el API con las siguientes operaciones:
Ordenes:
Obtener las ordenes
Obtener una orden en particular
Cancelar una orden
Pagar una orden

Productos
Obtener productos.
Editar productos.
Grear productos.
Eliminar productos.

El proyecto esta constituido de la siguiente manera:
- Proyectos:
![image](https://github.com/user-attachments/assets/96ce4e7c-1ab4-4ee0-bda0-3fdcb6caea88)

5 Proyectos para el manejo de las reglas del negocio.
1 Proyecto de pruebas.
1 Proyecto web con todas las funcionalidades.

APIs creados:
![Screenshot 2024-11-04 100259](https://github.com/user-attachments/assets/3dd85abd-94c6-476b-876f-48ef71d5f2e7)

Pantallas WEB:
![Screenshot 2024-11-04 100327](https://github.com/user-attachments/assets/8aca62f1-f521-406c-93eb-78a227d82814)
![Screenshot 2024-11-04 100350](https://github.com/user-attachments/assets/cc458afa-a3b5-4dd6-b131-e1fc047aaa1f)
![Screenshot 2024-11-04 100531](https://github.com/user-attachments/assets/29a68914-9cb0-4bec-8859-b2bc18df85c9)
![Screenshot 2024-11-04 100558](https://github.com/user-attachments/assets/f77f4030-1bc2-45f3-a1bd-78bf46e954e9)
![Screenshot 2024-11-04 100659](https://github.com/user-attachments/assets/5b3dc4dd-4638-4a58-ba55-a55655ee95f3)
![Screenshot 2024-11-04 100741](https://github.com/user-attachments/assets/03d50b99-fd88-4627-be49-3cba1dfe42dc)
![Screenshot 2024-11-04 100817](https://github.com/user-attachments/assets/b6d508d8-a161-488c-a417-3d368eb44523)

Notas:
1.- Para la ejecución del proyecto.
  Se debe correr el migration para la creación de la base de datos en el ambiente local, la estructura es la siguiente:
![image](https://github.com/user-attachments/assets/ad4cd9cc-b4bd-434f-865e-fc53b3c5a236)

La tabla Orders, almacena toda la información de las ordenes procesadas incluyendo el proveedor utilizado en la operación, dicho detalle se muestra en la pantalla ordenes de la web.

2.- Configurar el inicio de los proyectos API y Web, tal como se muestra en la siguiente imágen:
![image](https://github.com/user-attachments/assets/3759db02-8b58-464f-865b-9ae866e69cca)

3.- EL proyecto de test incluye solo algunas pruebas realizadas sobre algunos objetos, por temas de tiempo fué dificil agregar todos los escenarios de pruebas.

4.- Realizando pruebas sobre los APIs de los proveedores, he notados que el servicio de PagoFacil solo admite transacciones "Cash" y "Card", no admite "Transfer".
  El servicio del proveedor "CazaPagos", solo admite transacciones de tipo "Transfer" y "CreditCard".
  En los casos antes expuestos recí errores al momento de realizar pruebas sobre los servicios, de allí mis comentarios.

  



