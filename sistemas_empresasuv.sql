/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 50624
 Source Host           : localhost:3306
 Source Schema         : sistemas_empresasuv

 Target Server Type    : MySQL
 Target Server Version : 50624
 File Encoding         : 65001

 Date: 28/09/2023 12:50:34
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for auth_tokens
-- ----------------------------
DROP TABLE IF EXISTS `auth_tokens`;
CREATE TABLE `auth_tokens`  (
  `tiempo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `tipo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `usuario` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of auth_tokens
-- ----------------------------

-- ----------------------------
-- Table structure for claves_solicitud
-- ----------------------------
DROP TABLE IF EXISTS `claves_solicitud`;
CREATE TABLE `claves_solicitud`  (
  `time_request` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Tiempo de solicitud',
  `hash` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'Código hash de solicitud',
  `mail_user` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'Correo del usuario',
  `new_mail` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'Nuevo correo al cambiar',
  PRIMARY KEY (`time_request`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of claves_solicitud
-- ----------------------------
INSERT INTO `claves_solicitud` VALUES ('1679535784', 'MTY3OTUzNTc4NA==', 'rixio.iguaran@yahoo.com', NULL);
INSERT INTO `claves_solicitud` VALUES ('1681172766', 'MTY4MTE3Mjc2Ng==', 'elilee80@hotmail.com', NULL);

-- ----------------------------
-- Table structure for folios
-- ----------------------------
DROP TABLE IF EXISTS `folios`;
CREATE TABLE `folios`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `concepto` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `serie` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `folio` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = COMPACT;

-- ----------------------------
-- Records of folios
-- ----------------------------

-- ----------------------------
-- Table structure for tpv_cajas
-- ----------------------------
DROP TABLE IF EXISTS `tpv_cajas`;
CREATE TABLE `tpv_cajas`  (
  `id` int(9) NOT NULL AUTO_INCREMENT,
  `id_usuario` int(9) NOT NULL COMMENT 'Usuario que tiene asignada la caja',
  `nombre` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Nombre de la caja',
  `empresa_SDK` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Nombre de la empresa comercial',
  `concepto_SDK` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Código de concepto para documentos',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 14 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = COMPACT;

-- ----------------------------
-- Records of tpv_cajas
-- ----------------------------
INSERT INTO `tpv_cajas` VALUES (1, 1, 'Caja de pruebas', 'cmPuntoVentas', '3.4');
INSERT INTO `tpv_cajas` VALUES (2, 3, 'Xalapa CEU', 'cmTIENDA', '3.1');
INSERT INTO `tpv_cajas` VALUES (3, 12, 'Xalapa Museo', 'cmTIENDA_MAX', '3.2');
INSERT INTO `tpv_cajas` VALUES (4, 0, 'Xalapa Estadio', 'cmTIENDA', '3.3');
INSERT INTO `tpv_cajas` VALUES (5, 7, 'Veracruz', 'cmTIENDA', '3.4');
INSERT INTO `tpv_cajas` VALUES (6, 8, 'Poza Rica', 'cmTIENDA', '3.5');
INSERT INTO `tpv_cajas` VALUES (7, 10, 'Ixtaczoquitlán', 'cmTIENDA', '3.6');
INSERT INTO `tpv_cajas` VALUES (8, 11, 'Coatzacoalcos', 'cmTIENDA', '3.7');
INSERT INTO `tpv_cajas` VALUES (9, 11, 'Minatitlan', 'cmTIENDA', '3.8');
INSERT INTO `tpv_cajas` VALUES (10, 4, 'Xalapa CEU', 'cmTIENDA', '3.1');
INSERT INTO `tpv_cajas` VALUES (11, 2, 'Xalapa CEU', 'cmTIENDA', '3.1');
INSERT INTO `tpv_cajas` VALUES (12, 1, 'Librería UV', 'cmLibreriaUV', '4.1');
INSERT INTO `tpv_cajas` VALUES (13, 1, 'Caja de pruebas II', 'cmPuntoVentas', '3.5');

-- ----------------------------
-- Table structure for tpv_cajas_almacenes
-- ----------------------------
DROP TABLE IF EXISTS `tpv_cajas_almacenes`;
CREATE TABLE `tpv_cajas_almacenes`  (
  `id_caja` int(9) NULL DEFAULT NULL COMMENT 'id de la caja',
  `id_SDK` int(9) NULL DEFAULT NULL COMMENT 'id almacen en ContPAQ',
  `nombre_SDK` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'nombre almacen en ContPAQ'
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = COMPACT;

-- ----------------------------
-- Records of tpv_cajas_almacenes
-- ----------------------------
INSERT INTO `tpv_cajas_almacenes` VALUES (1, 120, 'Veracruz Ropa');
INSERT INTO `tpv_cajas_almacenes` VALUES (1, 121, 'Veracruz Promocionales');
INSERT INTO `tpv_cajas_almacenes` VALUES (1, 129, 'Veracruz Consignación');

-- ----------------------------
-- Table structure for tpv_cajas_cortes
-- ----------------------------
DROP TABLE IF EXISTS `tpv_cajas_cortes`;
CREATE TABLE `tpv_cajas_cortes`  (
  `id` int(9) NOT NULL AUTO_INCREMENT,
  `id_caja` int(9) NOT NULL,
  `fecha_inicio` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `fecha_cierre` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `m10c` int(11) NULL DEFAULT NULL COMMENT 'Monedas de 10 centavos',
  `m20c` int(11) NULL DEFAULT NULL COMMENT 'Monedas de 20 centavos',
  `m50c` int(11) NULL DEFAULT NULL COMMENT 'Monedas de 50 centavos',
  `m1p` int(11) NULL DEFAULT NULL COMMENT 'Monedas de 1 peso',
  `m2p` int(11) NULL DEFAULT NULL COMMENT 'Monedas de 2 peso',
  `m5p` int(11) NULL DEFAULT NULL COMMENT 'Monedas de 5 peso',
  `m10p` int(11) NULL DEFAULT NULL COMMENT 'Monedas de 10 peso',
  `m20p` int(11) NULL DEFAULT NULL COMMENT 'Monedas de 20 peso',
  `b20p` int(11) NULL DEFAULT NULL COMMENT 'Billetes de 20 pesos',
  `b50p` int(11) NULL DEFAULT NULL COMMENT 'Billetes de 50 pesos',
  `b100p` int(11) NULL DEFAULT NULL COMMENT 'Billetes de 100 pesos',
  `b200p` int(11) NULL DEFAULT NULL COMMENT 'Billetes de 200 pesos',
  `b500p` int(11) NULL DEFAULT NULL COMMENT 'Billetes de 500 pesos',
  `b1000p` int(11) NULL DEFAULT NULL COMMENT 'Billetes de 1000 pesos',
  `dinero_monedas` double NULL DEFAULT NULL,
  `dinero_billetes` double NULL DEFAULT NULL,
  `dinero_inicio` double NOT NULL,
  `dinero_cierre` double NOT NULL,
  `utilidad_neta` double NOT NULL,
  `numero_operaciones` int(9) NULL DEFAULT NULL,
  `numero_productos` int(9) NULL DEFAULT NULL,
  `estado` varchar(2) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'A=Abierto C=Cerrado',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = COMPACT;

-- ----------------------------
-- Records of tpv_cajas_cortes
-- ----------------------------
INSERT INTO `tpv_cajas_cortes` VALUES (1, 1, '1693487704', '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, 'A');
INSERT INTO `tpv_cajas_cortes` VALUES (6, 12, '1693506296', '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, 'A');

-- ----------------------------
-- Table structure for tpv_sucursales
-- ----------------------------
DROP TABLE IF EXISTS `tpv_sucursales`;
CREATE TABLE `tpv_sucursales`  (
  `id` int(11) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of tpv_sucursales
-- ----------------------------

-- ----------------------------
-- Table structure for tpv_ventas
-- ----------------------------
DROP TABLE IF EXISTS `tpv_ventas`;
CREATE TABLE `tpv_ventas`  (
  `id` int(9) NOT NULL AUTO_INCREMENT,
  `id_corte` int(9) NOT NULL,
  `serie` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `folio` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `fecha` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `cliente` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `descuentos` float NULL DEFAULT NULL,
  `total` float NOT NULL,
  `tipopago` varchar(2) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `tipoventa` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `estado` varchar(2) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = COMPACT;

-- ----------------------------
-- Records of tpv_ventas
-- ----------------------------
INSERT INTO `tpv_ventas` VALUES (3, 1, 'VERACRUZ', '84', '1694120490', 'XAXX010101000', NULL, 0, '', '', 'A');
INSERT INTO `tpv_ventas` VALUES (4, 1, 'VERACRUZ', '85', '1694634482', 'XAXX010101000', NULL, 0, '', '', 'A');
INSERT INTO `tpv_ventas` VALUES (5, 1, '', '', '1694976002', '', NULL, 0, '', '', 'A');

-- ----------------------------
-- Table structure for tpv_ventas_aux
-- ----------------------------
DROP TABLE IF EXISTS `tpv_ventas_aux`;
CREATE TABLE `tpv_ventas_aux`  (
  `id` int(9) NOT NULL AUTO_INCREMENT,
  `id_venta` int(9) NOT NULL,
  `cantidad` int(9) NOT NULL,
  `codigo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `producto` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `catacteristicas` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `almacen` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `precio` float NOT NULL,
  `descuento` int(9) NULL DEFAULT NULL,
  `total` float NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = COMPACT;

-- ----------------------------
-- Records of tpv_ventas_aux
-- ----------------------------

-- ----------------------------
-- Table structure for usuarios
-- ----------------------------
DROP TABLE IF EXISTS `usuarios`;
CREATE TABLE `usuarios`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `correo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `paterno` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `materno` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `nombre` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `tel_movil` int(10) NULL DEFAULT NULL,
  `usuario` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `secreto` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ua_time` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0',
  `ua_ip` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0',
  `ua_host` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0',
  `estado` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`, `usuario`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of usuarios
-- ----------------------------
INSERT INTO `usuarios` VALUES (1, 'riguaran@empresasuv.mx', 'Iguarán', 'Chourio', 'Rixio Danilo', NULL, 'Administrador', '$2y$10$pxkdXGNEt6.nONjvfLv2Uu0MZoCYoY.O0noHxucP7Ay5.7bpwA0/O', '1695754123', '187.189.83.36', '192.168.1.200', 'A');

-- ----------------------------
-- Table structure for usuarios_empresas
-- ----------------------------
DROP TABLE IF EXISTS `usuarios_empresas`;
CREATE TABLE `usuarios_empresas`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id del acceso',
  `id_usuario` int(11) NOT NULL COMMENT 'id del usuario',
  `op_empresas` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'id de la empresa',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of usuarios_empresas
-- ----------------------------
INSERT INTO `usuarios_empresas` VALUES (1, 1, '0');

SET FOREIGN_KEY_CHECKS = 1;
