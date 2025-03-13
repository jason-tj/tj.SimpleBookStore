# �������(Demo)

## ��Ŀ����

����Ŀ��һ������ ASP.NET Core 8 ���������ϵͳ���ṩ���鼮�������ﳵ���������������Լ��û������֤�ȹ��ܡ���Ŀ���� RESTful API ��ƣ�ʹ�� JWT ���������֤����Ȩ����ͨ�� OpenTelemetry �ռ�ң�����ݡ�

## �����б�

### 1. �鼮����

- **����鼮**������Ա����������鼮��
- **��ȡ�鼮**���û����Բ鿴�����鼮����� ID ��ȡ�ض��鼮��

### 2. ���ﳵ����

- **����鼮�����ﳵ**���û����Խ��鼮��ӵ����ﳵ��
- **�鿴���ﳵ**���û����Բ鿴���ﳵ�е��鼮��

### 3. ��������

- **�����ܼ�**���û����Լ��㹺�ﳵ���鼮���ܼۣ������ɶ�����
- **�鿴����**���û����Բ鿴������ʷ������

### 4. �û������֤����Ȩ

- **�û���¼**���û���¼���ȡ JWT Token��
- **��ɫ����**��֧�ֹ���Ա����ͨ�û���ɫ��
- **Ȩ�޿���**��ֻ�е�¼�û����ܷ��ʹ��ﳵ�Ͷ������ܡ�

### 5. ң�������ռ�

- **OpenTelemetry**���ռ�Ӧ�ó���ĸ��١�ָ�����־���ݡ�
- **����������̨**����ң���������������̨��

### 6. Docker����������

- ����ʱ��ͱ����������⣬δ������ز����Լ�Dockerfile�ļ�����ȷ��

------

## ����ջ

- **���**��ASP.NET Core 8
- **���ݿ�**��Entity Framework Core + �ڴ����ݿ⣨����������
- **�����֤**��JWT + ASP.NET Core Identity
- **ң������**��OpenTelemetry
- **���Կ��**��xUnit + Moq

## ��Ŀ�ṹ

```
tj.SimpleBookStore/
������ Controllers/              # API ������
��   ������ BooksController.cs     # �鼮����
��   ������ CartController.cs      # ���ﳵ����
��   ������ CheckoutController.cs  # ��������
��   ������ AuthController.cs      # �û������֤
������ Models/                   # ����ģ��
��   ������ Book.cs                # �鼮ģ��
��   ������ CartItem.cs            # ���ﳵ��ģ��
��   ������ Order.cs               # ����ģ��
��   ������ User.cs            # �û���Ϣģ��
������ Services/                 # ҵ���߼�����
��   ������ IBookService.cs        # �鼮����ӿ�
��   ������ BookService.cs         # �鼮����ʵ��
��   ������ ICartService.cs        # ���ﳵ����ӿ�
��   ������ CartService.cs         # ���ﳵ����ʵ��
��   ������ ICheckoutService.cs    # ��������ӿ�
��   ������ CheckoutService.cs     # ��������ʵ��
������ Repositories/             # ���ݷ��ʲ�
��   ������ IBookRepository.cs     # �鼮�ֿ�ӿ�
��   ������ BookRepository.cs      # �鼮�ֿ�ʵ��
��   ������ ICartRepository.cs     # ���ﳵ�ֿ�ӿ�
��   ������ CartRepository.cs      # ���ﳵ�ֿ�ʵ��
������ DbContexts/                     # ���ݿ�������
��   ������ ApplicationDbContext.cs # EF Core ���ݿ�������
������ DTOs/                     # ���ݴ������
��   ������ BookDto.cs             # �鼮 DTO
��   ������ CartItemDto.cs         # ���ﳵ�� DTO
������ Tests/                    # ��Ԫ���Ժͼ��ɲ���
��   ������ UnitTests/             # ��Ԫ����
��   ������ IntegrationTests/      # ���ɲ���
������ Program.cs                # Ӧ�ó������
������ Dockerfile                # Dockerfile
������ README.md                 # ��Ŀ�����ĵ�
```

## ���ٿ�ʼ

### 1. ��¡��Ŀ

```
git clone https://github.com/jason-tj/tj.SimpleBookStore.git
cd online-bookstore
```

### 2. ��װ����

```
dotnet restore
```

### 3. ������Ŀ

```
dotnet run
```

### 4. ���� API

- **Swagger UI**��`http://localhost:44386/swagger`

## API �ĵ�

### Ȩ�޹���

#### ��ȡtoken

- **URL**: `/api/Auth/token?username={username}`

- **Method**: `GET`

- **Response**:

  ```
    {
      "token": "ajshdjkashdkjasdh",
    }
  ```

### �鼮����

#### ��ȡ�����鼮

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

#### ����鼮

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

### ���ﳵ����

#### ����鼮�����ﳵ

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

#### �鿴���ﳵ

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

### ��������

#### �����ܼ۲�����

- **URL**: `/api/checkout/Checkout`

- **Method**: `GET`

- **Response**:

  ```
  {
    "total": 40.0
  }
  ```

#### ��ȡ�û������б�

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
          "title": "��һ��",
          "author": "tj",
          "price": 100,
          "category": "sad"
        }
      }
    ]
  }
]
```

## ����

### ���е�Ԫ����

```
dotnet test
```

### ���м��ɲ���

```
dotnet test --filter Category=Integration
```

## ����

����ʱ�����⣬����Ŀ��û�в�ȡ��Ŀ�ܹ��ķֲ���ƣ�Ҳû�н���docker��������������ԣ��û�����ֱ��ʹ����IdentityUser�Ľṹ��û������Ե�����û������¼���̡�Ԥ���������û�general��admin����֧���������û���ȡtoken֮��ʹ�ú����ӿڣ���token���ݸ���ճ��֮����swagger��ֱ�ӵ��ü��ɡ�