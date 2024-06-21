import cv2


img_source = cv2.imread('sudoku.jpg', 0)

ret,img_result1 = cv2.threshold(img_source, 0, 255, cv2.THRESH_BINARY+cv2.THRESH_OTSU)
img_result2 = cv2.adaptiveThreshold(img_source, 255, cv2.ADAPTIVE_THRESH_MEAN_C, cv2.THRESH_BINARY, 21, 5)
img_result3 = cv2.adaptiveThreshold(img_source, 255, cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY, 21, 5)



cv2.imshow("SOURCE", img_source)
cv2.imshow("THRESH_BINARY_OTSU", img_result1)
cv2.imshow("ADAPTIVE_THRESH_MEAN_C", img_result2)
cv2.imshow("ADAPTIVE_THRESH_GAUSSIAN_C", img_result3)

cv2.waitKey(0)
cv2.destroyAllWindows()
