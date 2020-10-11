import 'package:distributed_orders_flutter/domain/models/address.dart';
import 'package:distributed_orders_flutter/domain/models/order_item.dart';
import 'package:json_annotation/json_annotation.dart';

part 'order.g.dart';

@JsonSerializable()
class Order {
  String id;
  String orderId;
  int status;
  Address deliveryAddress;
  List<OrderItem> orderItems;

  Order();

  factory Order.fromJson(Map<String, dynamic> json) {
    return _$OrderFromJson(json);
  }

  Map<String, dynamic> toJson() {
    return _$OrderToJson(this);
  }
}