// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'product.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Product _$ProductFromJson(Map<String, dynamic> json) {
  return Product(
    json['productId'] as String,
    json['availableQuantity'] as int,
    json['reservedQuantity'] as int,
  );
}

Map<String, dynamic> _$ProductToJson(Product instance) => <String, dynamic>{
      'productId': instance.productId,
      'availableQuantity': instance.availableQuantity,
      'reservedQuantity': instance.reservedQuantity,
    };
