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

 Date: 30/08/2023 09:32:26
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for ventas
-- ----------------------------
DROP TABLE IF EXISTS `ventas`;
CREATE TABLE `ventas`  (
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
) ENGINE = InnoDB AUTO_INCREMENT = 2195 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

SET FOREIGN_KEY_CHECKS = 1;
