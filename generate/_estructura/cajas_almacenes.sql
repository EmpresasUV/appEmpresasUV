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

 Date: 30/08/2023 09:32:09
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for cajas_almacenes
-- ----------------------------
DROP TABLE IF EXISTS `cajas_almacenes`;
CREATE TABLE `cajas_almacenes`  (
  `id_caja` int(9) NULL DEFAULT NULL COMMENT 'id de la caja',
  `id_SDK` int(9) NULL DEFAULT NULL COMMENT 'id almacen en ContPAQ',
  `nombre_SDK` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'nombre almacen en ContPAQ'
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

SET FOREIGN_KEY_CHECKS = 1;
