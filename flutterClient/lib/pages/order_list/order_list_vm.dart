import 'package:distributed_orders_flutter/domain/models/order.dart';
import 'package:distributed_orders_flutter/domain/services/order_service.dart';
import 'package:mobx/mobx.dart';

part 'order_list_vm.g.dart';

class OrderListVm = OrderListVmBase with _$OrderListVm;
abstract class OrderListVmBase with Store {
  final OrderService orderService;

  @observable
  List<Order> orders;

  OrderListVmBase(this.orderService);

  @action
  Future<void> getOrders() async {
    orders = await orderService.getOrders();
  }
}
