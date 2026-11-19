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
    CONSTRAINT FK_ProdProv_Proveedor FOREIGN KEY (ID_Proveedor) REFERENCES Proveedor(ID_Proveedor),
    CONSTRAINT UQ_Prod_Prov UNIQUE (ID_Producto, ID_Proveedor)
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


-- =====================================================
-- DATOS INICIALES (SEMILLAS DE PRUEBA LOCAL)
-- =====================================================

-- Usuarios de prueba
INSERT INTO Usuario (ID_Usuario, Nombre, Correo, ContrasenaHash, Rol, Estado) VALUES
(1, 'Administrador Urban', 'admin@admin.com', 'admin01', 'Admin', TRUE),
(2, 'Vendedor Principal', 'vendedor@urbanstock.com', 'vendedor01', 'Vendedor', TRUE)
ON DUPLICATE KEY UPDATE
    Nombre = VALUES(Nombre),
    Correo = VALUES(Correo),
    ContrasenaHash = VALUES(ContrasenaHash),
    Rol = VALUES(Rol),
    Estado = VALUES(Estado);

-- Categorías de prendas y accesorios urbanos
INSERT INTO Categoria (ID_Categoria, NombreCategoria) VALUES
(1, 'Camisetas Oversize'),
(2, 'Hoodies & Buzos'),
(3, 'Pantalones Cargo & Joggers'),
(4, 'Gorras & Beanies'),
(5, 'Accesorios & Joyería')
ON DUPLICATE KEY UPDATE
    NombreCategoria = VALUES(NombreCategoria);

-- Proveedores locales y de confección
INSERT INTO Proveedor (ID_Proveedor, NombreEmpresa, NombreContacto, Telefono) VALUES
(1, 'Streetwear Colombia S.A.S', 'Carlos Mendoza', '3105550192'),
(2, 'Textil Urban Medellín', 'Laura Restrepo', '3014449281'),
(3, 'Importadora Caps & Accessories', 'Andrés Morales', '3207771234')
ON DUPLICATE KEY UPDATE
    NombreEmpresa = VALUES(NombreEmpresa),
    NombreContacto = VALUES(NombreContacto),
    Telefono = VALUES(Telefono);

-- Clientes frecuentes iniciales
INSERT INTO Cliente (ID_Cliente, Documento, NombreCompleto, Telefono, Correo) VALUES
(1, '1020304050', 'Juan Pérez', '3112223344', 'juan.perez@example.com'),
(2, '9876543210', 'Valentina Gómez', '3158889900', 'valentina.g@example.com'),
(3, '1122334455', 'Mateo Ramírez', '3001112233', 'mateo.r@example.com')
ON DUPLICATE KEY UPDATE
    Documento = VALUES(Documento),
    NombreCompleto = VALUES(NombreCompleto),
    Telefono = VALUES(Telefono),
    Correo = VALUES(Correo);

-- Catálogo de productos con stock variado (incluye stock normal, bajo y agotado)
INSERT INTO Producto (ID_Producto, ID_Categoria, Nombre, Talla, PrecioCosto, PrecioVenta, Stock, StockMinimo) VALUES
(1, 1, 'Camiseta Oversize Tokyo Drift', 'L', 35000.00, 75000.00, 15, 3),
(2, 1, 'Camiseta Acid Wash Graphic', 'M', 38000.00, 80000.00, 2, 4), -- Alerta Stock Bajo
(3, 2, 'Hoodie Heavyweight Vintage 90s', 'XL', 65000.00, 145000.00, 8, 3),
(4, 2, 'Hoodie Boxy Fit Charcoal', 'M', 62000.00, 139000.00, 1, 3), -- Alerta Stock Bajo
(5, 3, 'Pantalón Cargo Multi-Pocket Negro', '32', 55000.00, 120000.00, 10, 3),
(6, 3, 'Jogger Táctico Cyberpunk', '30', 50000.00, 115000.00, 0, 2), -- Agotado
(7, 4, 'Gorra Snapback Urban King', 'Única', 25000.00, 55000.00, 20, 5),
(8, 4, 'Beanie Tejido Ribbed Negro', 'Única', 18000.00, 42000.00, 3, 4), -- Alerta Stock Bajo
(9, 5, 'Cadena Eslabón Cubano Acero Inox', 'Única', 20000.00, 49000.00, 12, 4),
(10, 5, 'Riñonera Crossbody Reflectiva', 'Única', 28000.00, 65000.00, 6, 3)
ON DUPLICATE KEY UPDATE
    ID_Categoria = VALUES(ID_Categoria),
    Nombre = VALUES(Nombre),
    Talla = VALUES(Talla),
    PrecioCosto = VALUES(PrecioCosto),
    PrecioVenta = VALUES(PrecioVenta),
    Stock = VALUES(Stock),
    StockMinimo = VALUES(StockMinimo);

-- Relación Producto - Proveedor
INSERT INTO Producto_Proveedor (ID_ProductoProveedor, ID_Producto, ID_Proveedor) VALUES
(1, 1, 1),
(2, 2, 1),
(3, 3, 2),
(4, 4, 2),
(5, 5, 2),
(6, 6, 1),
(7, 7, 3),
(8, 8, 3),
(9, 9, 3),
(10, 10, 3)
ON DUPLICATE KEY UPDATE
    ID_Producto = VALUES(ID_Producto),
    ID_Proveedor = VALUES(ID_Proveedor);

-- Ventas iniciales de prueba (Historial transaccional de ejemplo)
INSERT INTO Venta (ID_Venta, ID_Usuario, ID_Cliente, FechaHora, TotalVenta) VALUES
(1, 1, 1, '2026-11-15 14:30:00', 220000.00),
(2, 2, NULL, '2026-11-17 17:15:00', 104000.00)
ON DUPLICATE KEY UPDATE
    ID_Usuario = VALUES(ID_Usuario),
    ID_Cliente = VALUES(ID_Cliente),
    FechaHora = VALUES(FechaHora),
    TotalVenta = VALUES(TotalVenta);

-- Detalles de las ventas iniciales
INSERT INTO DetalleVenta (ID_Detalle, ID_Venta, ID_Producto, Cantidad, PrecioUnitario, Subtotal) VALUES
(1, 1, 1, 1, 75000.00, 75000.00),
(2, 1, 3, 1, 145000.00, 145000.00),
(3, 2, 7, 1, 55000.00, 55000.00),
(4, 2, 9, 1, 49000.00, 49000.00)
ON DUPLICATE KEY UPDATE
    ID_Venta = VALUES(ID_Venta),
    ID_Producto = VALUES(ID_Producto),
    Cantidad = VALUES(Cantidad),
    PrecioUnitario = VALUES(PrecioUnitario),
    Subtotal = VALUES(Subtotal);