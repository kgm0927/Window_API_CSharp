// ImageMorphology.cpp: implementation of the CImageMorphology class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "skkDib.h"
#include "Image.h"
#include "ImageMorphology.h"
#include "ImagePixel.h"
#include "ProgressBar.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CImageMorphology::CImageMorphology()
{
	m_pImage = NULL;
}

CImageMorphology::~CImageMorphology()
{

}

CImageMorphology::CImageMorphology(CImage &Image)
{
	m_pImage = &Image;
	m_nWidth = m_pImage->GetWidth();
	m_nHeight = m_pImage->GetHeight();
	m_nDepth = m_pImage->GetBitCount();
}

void CImageMorphology::BErosion()
{
	ASSERT(m_pImage);
	ASSERT(m_pImage->GetHandle() != NULL);

	if(m_nDepth == 8) ErosionBinary();
	else AfxMessageBox("Gray images only");
}

void CImageMorphology::ErosionBinary()
{
	CProgressBar bar(_T("Erosion"), 100, m_nHeight);
	CPixelPtr ptrSorce(m_pImage->GetUndoHandle());
	CPixelPtr ptrDest(*m_pImage);

	unsigned char temp[3][3];//����ũó���� ���� ����
	unsigned char mask[3][3]={255,255,255,255,255,255,255,255,255};//ħ�Ŀ����� ���� ����ũ
	int sum=0;

	for(int y=0;y<m_nHeight-2;y++)
	{
		for(int x=0;x<m_nWidth-2;x++)
		{
			for(int row=0;row<3;row++)
			{
				for(int column=0;column<3;column++)
				{
					temp[row][column]=ptrSorce[row+y][column+x];
				}
			}//����ũó���� �ҷ��� 3x3�� �̹����� �и�
			
			for(int i=0;i<3;i++)
			{
				for(int j=0;j<3;j++)
				{
					if(temp[i][j]==mask[i][j])
					{
						sum ++;	//����ũ�� �и��س� 3x3�� ��ġ�Ǵ� ����.
					}
				}
			}
			//��� ��ƾ �κ�.
			if(sum == 9)	//default 9							
			{
				ptrDest[1+y][1+x]=255;//��ġ������ 9�̻��̸� 3x3����� 255(���)�Ҵ�.
			}
			else
			{
				ptrDest[1+y][1+x]=0;//��ġ������ 9�̸��̸� 3x3����� 0(������)�Ҵ�.
			}
			sum=0;
		}
		bar.StepIt();
	}
}

void CImageMorphology::BDilation()
{
	ASSERT(m_pImage);
	ASSERT(m_pImage->GetHandle() != NULL);

	if(m_nDepth == 8) DilationBinary();
	else AfxMessageBox("Gray images only");
}

void CImageMorphology::DilationBinary()
{
	CProgressBar bar(_T("Erosion"), 100, m_nHeight);
	CPixelPtr ptrSource(m_pImage->GetUndoHandle());
	CPixelPtr ptrDest(*m_pImage);

	unsigned char temp[3][3];
	unsigned char mask[3][3]={0,0,0,0,0,0,0,0,0};
	int sum=0;

	for(int y=0;y<m_nHeight-2;y++)
	{
		for(int x=0;x<m_nWidth-2;x++)
		{
			for(int row=0;row<3;row++)
			{
				for(int column=0;column<3;column++)
				{
					temp[row][column]=ptrSource[row+y][column+x];
				}
			}//�̹��� �и�.3x3
			//Step 1
			
			for(int i=0;i<3;i++)
			{
				for(int j=0;j<3;j++)
				{
					if(temp[i][j]==mask[i][j])
					{
						sum ++;
					}
				}
			}
			//Step 2
			if(sum ==9)	//default 9
			{
				ptrDest[1+y][1+x]=0;
			}
			else
			{
				ptrDest[1+y][1+x]=255;
			}
			sum=0;
		}
		bar.StepIt();
	}//Step 3
}

void CImageMorphology::BOpening()
{
	ASSERT(m_pImage);
	ASSERT(m_pImage->GetHandle() != NULL);

	if(m_nDepth == 8) OpeningBinary();
	else AfxMessageBox("Gray images only");
}

void CImageMorphology::OpeningBinary()
{
	ErosionBinary();
	m_pImage->PrepareUndo(); 
	//Erosion result image �� undo image�� copy �Ѵ�. 
	//undo image�� ������ Dilation�� �����ϱ� ����  
	DilationBinary();
}

void CImageMorphology::BClosing()
{
	ASSERT(m_pImage);
	ASSERT(m_pImage->GetHandle() != NULL);

	if(m_nDepth == 8) ClosingBinary();
	else AfxMessageBox("Gray images only");
}

void CImageMorphology::ClosingBinary()
{
	DilationBinary();
	m_pImage->PrepareUndo(); 
	ErosionBinary();
}

