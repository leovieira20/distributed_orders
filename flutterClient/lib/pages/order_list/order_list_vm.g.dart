// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'order_list_vm.dart';

// **************************************************************************
// StoreGenerator
// **************************************************************************

// ignore_for_file: non_constant_identifier_names, unnecessary_brace_in_string_interps, unnecessary_lambdas, prefer_expression_function_bodies, lines_longer_than_80_chars, avoid_as, avoid_annotating_with_dynamic

mixin _$OrderListVm on OrderListVmBase, Store {
  final _$ordersAtom = Atom(name: 'OrderListVmBase.orders');

  @override
  List<Order> get orders {
    _$ordersAtom.reportRead();
    return super.orders;
  }

  @override
  set orders(List<Order> value) {
    _$ordersAtom.reportWrite(value, super.orders, () {
      super.orders = value;
    });
  }

  final _$getOrdersAsyncAction = AsyncAction('OrderListVmBase.getOrders');

  @override
  Future<void> getOrders() {
    return _$getOrdersAsyncAction.run(() => super.getOrders());
  }

  @override
  String toString() {
    return '''
orders: ${orders}
    ''';
  }
}
