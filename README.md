# 在线书店(Demo)

## 项目概述

本项目是一个基于 ASP.NET Core 8 的在线书店系统，提供了书籍管理、购物车操作、订单结算以及用户身份验证等功能。项目采用 RESTful API 设计，使用 JWT 进行身份验证和授权，并通过 OpenTelemetry 收集遥测数据。

## 功能列表

### 1. 书籍管理

- **添加书籍**：管理员可以添加新书籍。
- **获取书籍**：用户可以查看所有书籍或根据 ID 获取特定书籍。

### 2. 购物车操作

- **添加书籍到购物车**：用户可以将书籍添加到购物车。
- **查看购物车**：用户可以查看购物车中的书籍。

### 3. 订单结算

- **计算总价**：用户可以计算购物车中书籍的总价，并生成订单。
- **查看订单**：用户可以查看所有历史订单。

### 4. 用户身份验证与授权

- **用户登录**：用户登录后获取 JWT Token。
- **角色管理**：支持管理员和普通用户角色。
- **权限控制**：只有登录用户才能访问购物车和订单功能。

### 5. 遥测数据收集

- **OpenTelemetry**：收集应用程序的跟踪、指标和日志数据。
- **导出到控制台**：将遥测数据输出到控制台。

### 6. Docker容器化部署

- 由于时间和本机环境问题，未测试相关部署以及Dockerfile文件的正确性

------

## 技术栈

- **框架**：ASP.NET Core 8
- **数据库**：Entity Framework Core + 内存数据库（开发环境）
- **身份验证**：JWT + ASP.NET Core Identity
- **遥测数据**：OpenTelemetry
- **测试框架**：xUnit + Moq

## 项目结构

```
tj.SimpleBookStore/
├── Controllers/              # API 控制器
│   ├── BooksController.cs     # 书籍管理
│   ├── CartController.cs      # 购物车操作
│   ├── CheckoutController.cs  # 订单结算
│   └── AuthController.cs      # 用户身份验证
├── Models/                   # 数据模型
│   ├── Book.cs                # 书籍模型
│   ├── CartItem.cs            # 购物车项模型
│   ├── Order.cs               # 订单模型
│   └── User.cs            # 用户信息模型
├── Services/                 # 业务逻辑服务
│   ├── IBookService.cs        # 书籍服务接口
│   ├── BookService.cs         # 书籍服务实现
│   ├── ICartService.cs        # 购物车服务接口
│   ├── CartService.cs         # 购物车服务实现
│   ├── ICheckoutService.cs    # 订单服务接口
│   └── CheckoutService.cs     # 订单服务实现
├── Repositories/             # 数据访问层
│   ├── IBookRepository.cs     # 书籍仓库接口
│   ├── BookRepository.cs      # 书籍仓库实现
│   ├── ICartRepository.cs     # 购物车仓库接口
│   └── CartRepository.cs      # 购物车仓库实现
├── DbContexts/                     # 数据库上下文
│   └── ApplicationDbContext.cs # EF Core 数据库上下文
├── DTOs/                     # 数据传输对象
│   ├── BookDto.cs             # 书籍 DTO
│   └── CartItemDto.cs         # 购物车项 DTO
├── Tests/                    # 单元测试和集成测试
│   ├── UnitTests/             # 单元测试
│   └── IntegrationTests/      # 集成测试
├── Program.cs                # 应用程序入口
├── Dockerfile                # Dockerfile
└── README.md                 # 项目介绍文档
```

## 快速开始

### 1. 克隆项目

```
git clone https://github.com/jason-tj/tj.SimpleBookStore.git
cd online-bookstore
```

### 2. 安装依赖

```
dotnet restore
```

### 3. 运行项目

```
dotnet run
```

### 4. 访问 API

- **Swagger UI**：`http://localhost:44386/swagger`

## API 文档

### 权限管理

#### 获取token

- **URL**: `/api/Auth/token?username={username}`

- **Method**: `GET`

- **Response**:

  ```
    {
      "token": "ajshdjkashdkjasdh",
    }
  ```

### 书籍管理

#### 获取所有书籍

- **URL**: `/api/books/GetAllBooks`

- **Method**: `GET`

- **Response**:

  ```
  [
    {
      "id": 1,
      "title": "Book 1",
      "author": "Author 1",
      "price": 10.0,
      "category": "Fiction"
    }
  ]
  ```

#### 添加书籍

- **URL**: `/api/books/AddBook`

- **Method**: `POST`

- **Request Body**:

  ```
  {
    "title": "New Book",
    "author": "New Author",
    "price": 15.0,
    "category": "Science"
  }
  ```

### 购物车操作

#### 添加书籍到购物车

- **URL**: `/api/cart/AddToCart`

- **Method**: `POST`

- **Request Body**:

  ```
  {
    "bookId": 1,
    "quantity": 2
  }
  ```

- **Response**: `200 OK`

#### 查看购物车

- **URL**: `/api/cart/GetCart`

- **Method**: `GET`

- **Response**:

  ```
  [
    {
      "id": 1,
      "bookId": 1,
      "quantity": 2,
      "userId": "user1"
    }
  ]
  ```

### 订单结算

#### 计算总价并结账

- **URL**: `/api/checkout/Checkout`

- **Method**: `GET`

- **Response**:

  ```
  {
    "total": 40.0
  }
  ```

#### 获取用户订单列表

- **URL**: `/api/checkout/GetOrderList`
- **Method**: `POST`
- **Response**: `200 OK`

```
[
  {
    "id": 1,
    "userId": "c669b58c-b53b-4247-898b-39d34b5011b9",
    "createdTime": "2025-03-13T12:09:48.6037545+08:00",
    "totalAmount": 200,
    "orderItems": [
      {
        "id": 1,
        "orderId": 1,
        "bookId": 1,
        "quantity": 2,
        "price": 100,
        "book": {
          "id": 1,
          "title": "第一本",
          "author": "tj",
          "price": 100,
          "category": "sad"
        }
      }
    ]
  }
]
```

## 测试

### 运行单元测试

```
dotnet test
```

### 运行集成测试

```
dotnet test --filter Category=Integration
```

## 综述

由于时间问题，该项目并没有采取项目架构的分层设计，也没有进行docker的容器化部署测试，用户管理直接使用了IdentityUser的结构，没有针对性的设计用户密码登录流程。预设了两个用户general、admin，仅支持这两个用户获取token之后使用后续接口，将token内容复制粘贴之后在swagger中直接调用即可。