﻿namespace PocketMoney.Exceptions;

public class DataContextFactoryException(string objectName) : Exception($"Unable to create instance of {objectName}");