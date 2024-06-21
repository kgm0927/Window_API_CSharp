phone_book = { }
phone_book["홍길동"] = "010-1234-5678"
phone_book['강감찬'] = "010-1234-5679"
phone_book["이순신"] = '010-1234-5680'

for key in sorted(phone_book.keys()):
    print(key, phone_book[key])

# for keyy, valuee in phone_book.items() :
#     print(keyy, valuee, end=' ')  # p.279 중간참고

# print() #new line
# del phone_book['홍길동']
# print(phone_book)