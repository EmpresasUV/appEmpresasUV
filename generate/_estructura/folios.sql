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

 Date: 30/08/2023 09:32:17
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

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
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

SET FOREIGN_KEY_CHECKS = 1;
