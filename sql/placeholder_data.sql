-- Cliente de teste
INSERT INTO "customer" ("Id", "Name", "Email", "Cpf")
VALUES ('11111111-1111-1111-1111-111111111111', 'Cliente Teste', 'cliente@teste.com', '12345678901')
ON CONFLICT DO NOTHING;

-- Produtos de teste para lanchonete
INSERT INTO "products" ("Id", "Name", "Description", "Price", "ImageUrl", "Category")
VALUES 
('22222222-2222-2222-2222-222222222222', 'X-Burger', 'Hambúrguer clássico com queijo, alface e tomate', 20.00, 'https://img.com/xburger.png', ),
('33333333-3333-3333-3333-333333333333', 'X-Salada', 'Hambúrguer com salada e queijo', 22.00, 'https://img.com/xsalada.png', 'MainDish'),
('44444444-4444-4444-4444-444444444444', 'Batata Frita', 'Porção de batata frita crocante', 10.00, 'https://img.com/batata.png', 'SideDish'),
('55555555-5555-5555-5555-555555555555', 'Refrigerante', 'Lata 350ml de refrigerante', 6.00, 'https://img.com/refri.png', 'Drink'),
('66666666-6666-6666-6666-666666666666', 'Suco Natural', 'Suco natural de laranja 300ml', 8.00, 'https://img.com/suco.png', 'Drink')
ON CONFLICT DO NOTHING;