# FS.NHibernate - Como rodar

### Container MySql
1. Cria imagem MySql\
`docker build -t mysql-nhibernate-image -f Infra/Dockerfile .`

2. Cria container\
`docker run -d --rm --name mysql-nhibernate-container mysql-nhibernate-image`\
--com volume (para não perder os dados), e com porta (para expor na minha máquina)\
`docker run -d -v //c/projetos/FS.NHibernate/FS.NHibernate/FS.NHibernate/Infra/data://var/lib/mysql -p 3306:3306 --rm --name mysql-nhibernate-container mysql-nhibernate-image`

3. Verifica container\
`docker ps`

4. Entra no MySql\
`docker exec -it mysql-nhibernate-container /bin/bash`\
`mysql -uroot -pnhibernate`

5. Cria BD
```
CREATE DATABASE IF NOT EXISTS nhibernate;
USE nhibernate;

CREATE TABLE IF NOT EXISTS categories (
  id INT(11) AUTO_INCREMENT,
  name VARCHAR(255),
  PRIMARY KEY (id)
);

INSERT INTO categories VALUE(0, 'Course');
INSERT INTO categories VALUE(0, 'Pocket course');

CREATE TABLE IF NOT EXISTS products (
  id INT(11) AUTO_INCREMENT,
  name VARCHAR(255),
  price DECIMAL(10, 2),
  category_id INT(11),
  PRIMARY KEY (id),
  FOREIGN KEY fk_cat(category_id)
  REFERENCES categories(id)
  ON UPDATE CASCADE
  ON DELETE RESTRICT
);

INSERT INTO products VALUE(0, 'Curso Front-end especialista', 2500, 1);
INSERT INTO products VALUE(0, 'Curso JS Fullstack', 900, 1);
INSERT INTO products VALUE(0, 'Mini curso Docker', 0, 2);

SELECT * FROM products p INNER JOIN categories c ON c.id = p.category_id;
```
6. Se quiser fazer pelo phpMyAdmin\
`docker run --name phpmyadmin-container -d --link mysql-container:db -p 8080:80 phpmyadmin/phpmyadmin`

