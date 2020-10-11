// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'order.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Order _$OrderFromJson(Map<String, dynamic> json) {
  return Order()
    ..id = json['id'] as String
    ..orderId = json['orderId'] as String
    ..status = json['status'] as int
    ..deliveryAddress = json['deliveryAddress'] == null
        ? null
        : Address.fromJson(json['deliveryAddress'] as Map<String, dynamic>)
    ..orderItems = (json['orderItems'] as List)
        ?.map((e) =>
            e == null ? null : OrderItem.fromJson(e as Map<String, dynamic>))
        ?.toList();
}

Map<String, dynamic> _$OrderToJson(Order instance) => <String, dynamic>{
      'id': instance.id,
      'orderId': instance.orderId,
      'status': instance.status,
      'deliveryAddress': instance.deliveryAddress,
      'orderItems': instance.orderItems,
    };
