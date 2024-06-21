// ImageMorphology.h: interface for the CImageMorphology class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_IMAGEMORPHOLOGY_H__E9291841_B2BB_11D4_8255_0000C0B11065__INCLUDED_)
#define AFX_IMAGEMORPHOLOGY_H__E9291841_B2BB_11D4_8255_0000C0B11065__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CImage;

class CImageMorphology  
{
public:
	CImageMorphology();
	CImageMorphology(CImage &Image);
	virtual ~CImageMorphology();
	void BErosion();
	void BDilation();
	void BOpening();
	void BClosing();


	
protected:
	int m_nWidth, m_nHeight, m_nDepth;
	CImage *m_pImage;
	
	void ErosionBinary();
	void DilationBinary();
	void OpeningBinary();
	void ClosingBinary();


};

#endif // !defined(AFX_IMAGEMORPHOLOGY_H__E9291841_B2BB_11D4_8255_0000C0B11065__INCLUDED_)
