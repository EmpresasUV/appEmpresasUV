/*
 Navicat Premium Data Transfer

 Source Server         : 192.168.1.10
 Source Server Type    : MySQL
 Source Server Version : 50624
 Source Host           : 192.168.1.10:3306
 Source Schema         : apppuntoventas

 Target Server Type    : MySQL
 Target Server Version : 50624
 File Encoding         : 65001

 Date: 30/08/2023 09:32:40
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for ventas_aux
-- ----------------------------
DROP TABLE IF EXISTS `ventas_aux`;
CREATE TABLE `ventas_aux`  (
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
) ENGINE = InnoDB AUTO_INCREMENT = 3021 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

SET FOREIGN_KEY_CHECKS = 1;
