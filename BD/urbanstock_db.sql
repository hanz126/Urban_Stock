CREATE DATABASE IF NOT EXISTS urbanstock_db
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE urbanstock_db;

-- 1. Tabla Usuario
CREATE TABLE Usuario (
    ID_Usuario INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Correo VARCHAR(100) NOT NULL UNIQUE,
    ContrasenaHash VARCHAR(256) NOT NULL,
    Rol ENUM('Admin', 'Vendedor') NOT NULL,
    Estado BOOLEAN DEFAULT TRUE NOT NULL
) ENGINE=InnoDB;

-- 2. Tabla Categoria
CREATE TABLE Categoria (
    ID_Categoria INT AUTO_INCREMENT PRIMARY KEY,
    NombreCategoria VARCHAR(50) NOT NULL
) ENGINE=InnoDB;

-- 3. Tabla Proveedor
CREATE TABLE Proveedor (
    ID_Proveedor INT AUTO_INCREMENT PRIMARY KEY,
    NombreEmpresa VARCHAR(100) NOT NULL,
    NombreContacto VARCHAR(100),
    Telefono VARCHAR(20)
) ENGINE=InnoDB;

-- 4. Tabla Cliente
CREATE TABLE Cliente (
    ID_Cliente INT AUTO_INCREMENT PRIMARY KEY,
    Documento VARCHAR(20) NOT NULL UNIQUE,
    NombreCompleto VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20),
    Correo VARCHAR(100)
) ENGINE=InnoDB;

-- 5. Tabla Producto
CREATE TABLE Producto (
    ID_Producto INT AUTO_INCREMENT PRIMARY KEY,
    ID_Categoria INT NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Talla VARCHAR(10) NOT NULL,
    PrecioCosto DECIMAL(10,2) NOT NULL,
    PrecioVenta DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    StockMinimo INT NOT NULL DEFAULT 3,
    CONSTRAINT FK_Producto_Categoria FOREIGN KEY (ID_Categoria) REFERENCES Categoria(ID_Categoria)
) ENGINE=InnoDB;

-- 6. Tabla Intermedia Producto_Proveedor (Relación Muchos a Muchos N:M)
CREATE TABLE Producto_Proveedor (
    ID_ProductoProveedor INT AUTO_INCREMENT PRIMARY KEY,
    ID_Producto INT NOT NULL,
    ID_Proveedor INT NOT NULL,
    CONSTRAINT FK_ProdProv_Producto FOREIGN KEY (ID_Producto) REFERENCES Producto(ID_Producto),
    CONSTRAINT FK_ProdProv_Proveedor FOREIGN KEY (ID_Proveedor) REFERENCES Proveedor(ID_Proveedor)
) ENGINE=InnoDB;

-- 7. Tabla Venta
CREATE TABLE Venta (
    ID_Venta INT AUTO_INCREMENT PRIMARY KEY,
    ID_Usuario INT NOT NULL,
    ID_Cliente INT NULL,
    FechaHora DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    TotalVenta DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_Venta_Usuario FOREIGN KEY (ID_Usuario) REFERENCES Usuario(ID_Usuario),
    CONSTRAINT FK_Venta_Cliente FOREIGN KEY (ID_Cliente) REFERENCES Cliente(ID_Cliente)
) ENGINE=InnoDB;

-- 8. Tabla DetalleVenta
CREATE TABLE DetalleVenta (
    ID_Detalle INT AUTO_INCREMENT PRIMARY KEY,
    ID_Venta INT NOT NULL,
    ID_Producto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_Detalle_Venta FOREIGN KEY (ID_Venta) REFERENCES Venta(ID_Venta),
    CONSTRAINT FK_Detalle_Producto FOREIGN KEY (ID_Producto) REFERENCES Producto(ID_Producto)
) ENGINE=InnoDB;