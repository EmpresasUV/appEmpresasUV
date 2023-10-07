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

 Date: 30/08/2023 09:31:49
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for cajas_cortes
-- ----------------------------
DROP TABLE IF EXISTS `cajas_cortes`;
CREATE TABLE `cajas_cortes`  (
  `id` int(9) NOT NULL AUTO_INCREMENT,
  `id_caja` int(9) NOT NULL,
  `fecha_inicio` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `fecha_cierre` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `dinero_inicio` double NOT NULL,
  `dinero_cierre` double NOT NULL,
  `utilidad_neta` double NOT NULL,
  `numero_operaciones` int(9) NULL DEFAULT NULL,
  `numero_productos` int(9) NULL DEFAULT NULL,
  `estado` varchar(2) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'A=Abierto C=Cerrado',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 146 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

SET FOREIGN_KEY_CHECKS = 1;
