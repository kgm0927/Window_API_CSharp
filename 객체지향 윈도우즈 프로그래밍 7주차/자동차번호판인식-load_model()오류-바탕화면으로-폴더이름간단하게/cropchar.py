import cv2
import numpy as np
from operator import itemgetter


def crop_characters(img):

    contours, _ = cv2.findContours(
        img, 
        mode=cv2.RETR_LIST, 
        method=cv2.CHAIN_APPROX_SIMPLE
    )
    contours_dict = []

    for contour in contours:
        x, y, w, h = cv2.boundingRect(contour)
        
        # insert to dict
        contours_dict.append({
            'contour': contour,
            'x': x,
            'y': y,
            'w': w,
            'h': h,
            'cx': x + (w / 2),
            'cy': y + (h / 2)
        })
    area_average = 0
    for cont in contours_dict:
        area_average += cont['w'] * cont['h']
    area_average /= len(contours_dict)

    matched_list = []
    for cont in contours_dict:
        # 넓이가 80픽셀보다 크고,
        # 너비/높이 비율이 0.25보다 크고 0.7보다 작고,
        # 넓이가 평균 넓이의 반보다 크고,
        # 너비가 2픽셀보다 크고, 높이가 8픽셀 보다 큰 사각형만 남긴다.
        if cont['w'] * cont['h'] >= 80 and \
        0.25 <cont['w'] / cont['h'] < 0.7 and \
            cont['w'] * cont['h'] > area_average/2 and \
                cont['w'] > 2 and cont['h'] > 8:
            matched_list.append(cont)

    data = sorted(matched_list, key=itemgetter('x')) # x 좌표를 기준으로 재배치.
    cropped_numerics = []
    for r in data:
        cropped_numerics.append(img[r['y']:r['y']+r['h'], r['x']:r['x']+r['w']])
    
    return cropped_numerics