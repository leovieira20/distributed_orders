import 'package:json_annotation/json_annotation.dart';

part 'order_item.g.dart';

@JsonSerializable()
class OrderItem {
  String productId;
  int quantity;

  OrderItem(this.productId, this.quantity);

  factory OrderItem.fromJson(Map<String, dynamic> json) {
    return _$OrderItemFromJson(json);
  }

  Map<String, dynamic> toJson() {
    return _$OrderItemToJson(this);
  }
}