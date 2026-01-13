CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE Vendor (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(255) NOT NULL UNIQUE,
    email VARCHAR(255) NOT NULL UNIQUE,
    phone_number VARCHAR(20) NOT NULL UNIQUE,
    website_uRL VARCHAR(500),
    average_rate FLOAT DEFAULT 0.0,
    deleted_at TIMESTAMP,
    is_deleted BOOLEAN DEFAULT FALSE,
    creation_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    slug VARCHAR(255) UNIQUE,
    CHECK (Average_Rate >= 0 AND Average_Rate <= 5),
   CHECK (
  email ~* '^[A-Za-z0-9]+([._%+-][A-Za-z0-9]+)*@[A-Za-z0-9]+(-?[A-Za-z0-9]+)*(\.[A-Za-z]{2,})+$'
)
);

CREATE TABLE Vendor_Offer (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    product_id INT NOT NULL,
    vendor_id UUID NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    deleted_at TIMESTAMP,
    is_deleted BOOLEAN DEFAULT FALSE,
    creation_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    slug VARCHAR(255),
    FOREIGN KEY (product_id) REFERENCES Product(id) ON DELETE CASCADE,
    FOREIGN KEY (vendor_id) REFERENCES Vendor(id) ON DELETE CASCADE,
    CHECK (price > 0),
    UNIQUE (product_id, vendor_id)
);


CREATE TABLE Inventory (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    vendor_offer_id INT NOT NULL UNIQUE,
    quantity INT NOT NULL DEFAULT 0,
    reserved_quantity INT NOT NULL DEFAULT 0,
	creation_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (vendor_offer_id) REFERENCES Vendor_Offer(id) ON DELETE CASCADE,
    CHECK (quantity >= 0),
    CHECK (reserved_quantity >= 0),
    CHECK (reserved_quantity <= quantity)
);