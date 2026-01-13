CREATE TABLE Order_Status(
id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
status VARCHAR(255)
);

CREATE TABLE "Order" (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    total DECIMAL(10,2) NOT NULL,
    status_id INT NOT NULL,
    creation_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    slug VARCHAR(255),
    user_id UUID NOT NULL,
    FOREIGN KEY (status_id) REFERENCES Order_Status(id),
    FOREIGN KEY (user_id) REFERENCES "User"(id) ON DELETE CASCADE,
    CHECK (Total >= 0)
);

CREATE TABLE Order_Item (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    quantity INT NOT NULL,
    product_id INT NOT NULL,
    order_id INT NOT NULL,
    vendor_offer_id INT NOT NULL,
    product_name VARCHAR(255) NOT NULL,
    unit_price DECIMAL(10,2) NOT NULL,
    total_price DECIMAL(10,2) NOT NULL,
    creation_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    slug VARCHAR(255),
    FOREIGN KEY (product_id) REFERENCES Product(id) ON DELETE CASCADE,
    FOREIGN KEY (order_id) REFERENCES "Order"(id) ON DELETE CASCADE,
    FOREIGN KEY (vendor_offer_id) REFERENCES Vendor_Offer(id) ON DELETE CASCADE,
    CHECK (quantity > 0),
    CHECK (unit_price >= 0),
    CHECK (total_price >= 0),
    CHECK (total_price = unit_price * quantity)
);

CREATE TABLE Order_Shipping_Address (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    order_id INT NOT NULL UNIQUE,
    address VARCHAR(500) NOT NULL,
    city VARCHAR(100) NOT NULL,
    country VARCHAR(100) NOT NULL,
    location VARCHAR(255),
    mobile VARCHAR(20) NOT NULL,
	creation_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (order_id) REFERENCES "Order"(id) ON DELETE CASCADE
);
