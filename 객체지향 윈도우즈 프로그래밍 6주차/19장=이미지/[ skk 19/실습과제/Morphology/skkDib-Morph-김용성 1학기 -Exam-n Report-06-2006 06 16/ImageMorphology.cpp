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

	unsigned char temp[3][3];//마스크처리를 위한 버퍼
	unsigned char mask[3][3]={255,255,255,255,255,255,255,255,255};//침식연산을 위한 마스크
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
			}//마스크처리를 할려고 3x3씩 이미지를 분리
			
			for(int i=0;i<3;i++)
			{
				for(int j=0;j<3;j++)
				{
					if(temp[i][j]==mask[i][j])
					{
						sum ++;	//마스크와 분리해낸 3x3과 일치되는 정도.
					}
				}
			}
			//출력 루틴 부분.
			if(sum == 9)	//default 9							
			{
				ptrDest[1+y][1+x]=255;//일치정도가 9이상이면 3x3가운데에 255(흰색)할당.
			}
			else
			{
				ptrDest[1+y][1+x]=0;//일치정도가 9미만이면 3x3가운데에 0(검정색)할당.
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
			}//이미지 분리.3x3
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
	//Erosion result image 를 undo image로 copy 한다. 
	//undo image를 가지고 Dilation을 수행하기 때문  
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

