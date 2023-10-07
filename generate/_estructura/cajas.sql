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

 Date: 30/08/2023 09:31:59
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for cajas
-- ----------------------------
DROP TABLE IF EXISTS `cajas`;
CREATE TABLE `cajas`  (
  `id` int(9) NOT NULL AUTO_INCREMENT,
  `id_usuario` int(9) NOT NULL COMMENT 'Usuario que tiene asignada la caja',
  `nombre` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Nombre de la caja',
  `empresa_SDK` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Nombre de la empresa comercial',
  `concepto_SDK` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Código de concepto para documentos',
  `cp` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

SET FOREIGN_KEY_CHECKS = 1;
