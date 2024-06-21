// skkDibDoc.cpp : implementation of the CSkkDibDoc class
//

#include "stdafx.h"
#include "skkDib.h"

#include "skkDibDoc.h"
#include "ImageMorphology.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

CWait::CWait(CSkkDibDoc *pDoc)
{
	m_pDoc = pDoc;
	m_pDoc->BeginWaitCursor();
	m_pDoc->m_Image.PrepareUndo();
	m_pDoc->SetModifiedFlag();
}

CWait::~CWait()
{
//	m_pDoc->m_bRegionSelectMode = FALSE;
//	m_pDoc->m_Tracker.m_rect = CRect(0,0,0,0);
	m_pDoc->UpdateAllViews(NULL);
	m_pDoc->EndWaitCursor();
}
/////////////////////////////////////////////////////////////////////////////
// CSkkDibDoc

IMPLEMENT_DYNCREATE(CSkkDibDoc, CDocument)

BEGIN_MESSAGE_MAP(CSkkDibDoc, CDocument)
	//{{AFX_MSG_MAP(CSkkDibDoc)
	ON_COMMAND(IDM_B_CLOSING, OnBClosing)
	ON_COMMAND(IDM_B_DILATION, OnBDilation)
	ON_COMMAND(IDM_B_EROSION, OnBErosion)
	ON_COMMAND(IDM_B_OPENING, OnBOpening)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSkkDibDoc construction/destruction

CSkkDibDoc::CSkkDibDoc()
{
	// TODO: add one-time construction code here

}

CSkkDibDoc::~CSkkDibDoc()
{
}

BOOL CSkkDibDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: add reinitialization code here
	// (SDI documents will reuse this document)

	return TRUE;
}



/////////////////////////////////////////////////////////////////////////////
// CSkkDibDoc serialization

void CSkkDibDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: add storing code here
	}
	else
	{
		// TODO: add loading code here
	}
}

/////////////////////////////////////////////////////////////////////////////
// CSkkDibDoc diagnostics

#ifdef _DEBUG
void CSkkDibDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CSkkDibDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CSkkDibDoc commands

BOOL CSkkDibDoc::OnOpenDocument(LPCTSTR lpszPathName) 
{
	if (!CDocument::OnOpenDocument(lpszPathName))
		return FALSE;
	
	// 이미지 파일 읽기
	if ( !m_Image.Load(lpszPathName) ) return FALSE;
	
	int bitcount = m_Image.GetBitCount();
	if(!(bitcount == 8 || bitcount == 24))
	{
		m_Image.Free();
		AfxMessageBox("이 프로그램은 트루 컬러와 그레이스케일 영상만 지원합니다.");
		return FALSE;
	}

	return TRUE;
}

BOOL CSkkDibDoc::OnSaveDocument(LPCTSTR lpszPathName) 
{
	// 이미지 저장하기
	if ( !m_Image.Save(lpszPathName) ) return FALSE;
	SetModifiedFlag(FALSE);  //skk insertion
	return TRUE;
}

void CSkkDibDoc::OnBErosion() 
{
	CWait wait(this);	
	CImageMorphology image(m_Image);
	image.BErosion();	
}

void CSkkDibDoc::OnBDilation() 
{
	CWait wait(this);	
	CImageMorphology image(m_Image);
	image.BDilation();		
}

void CSkkDibDoc::OnBOpening() 
{
	CWait wait(this);	
	CImageMorphology image(m_Image);
	image.BOpening();		
}

void CSkkDibDoc::OnBClosing() 
{
	CWait wait(this);	
	CImageMorphology image(m_Image);
	image.BClosing();	
}
