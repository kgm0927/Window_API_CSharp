// skkDibView.cpp : implementation of the CSkkDibView class
//

#include "stdafx.h"
#include "skkDib.h"

#include "skkDibDoc.h"
#include "skkDibView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CSkkDibView

IMPLEMENT_DYNCREATE(CSkkDibView, CScrollView)

BEGIN_MESSAGE_MAP(CSkkDibView, CScrollView)
	//{{AFX_MSG_MAP(CSkkDibView)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, CScrollView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, CScrollView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, CScrollView::OnFilePrintPreview)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSkkDibView construction/destruction

CSkkDibView::CSkkDibView()
{
	// TODO: add construction code here

}

CSkkDibView::~CSkkDibView()
{
}

BOOL CSkkDibView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CScrollView::PreCreateWindow(cs);
}

/////////////////////////////////////////////////////////////////////////////
// CSkkDibView drawing

void CSkkDibView::OnDraw(CDC* pDC)
{
	CSkkDibDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	// TODO: add draw code for native data here
	if ( !pDoc->m_Image.IsDataNull() )
	{
		CRect rcDIB;
		rcDIB.top = rcDIB.left = 0;
		rcDIB.right = pDoc->m_Image.GetWidth();
		rcDIB.bottom = pDoc->m_Image.GetHeight();
		pDoc->m_Image.Draw(pDC->m_hDC,&rcDIB,&rcDIB);
	}
}

void CSkkDibView::OnInitialUpdate()
{
	CScrollView::OnInitialUpdate();
	CSkkDibDoc* pDoc = GetDocument();

	SetScrollSizes(MM_TEXT, pDoc->m_Image.GetSize());
	ResizeParentToFit();
}

/////////////////////////////////////////////////////////////////////////////
// CSkkDibView printing

BOOL CSkkDibView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CSkkDibView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CSkkDibView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}

/////////////////////////////////////////////////////////////////////////////
// CSkkDibView diagnostics

#ifdef _DEBUG
void CSkkDibView::AssertValid() const
{
	CScrollView::AssertValid();
}

void CSkkDibView::Dump(CDumpContext& dc) const
{
	CScrollView::Dump(dc);
}

CSkkDibDoc* CSkkDibView::GetDocument() // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CSkkDibDoc)));
	return (CSkkDibDoc*)m_pDocument;
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CSkkDibView message handlers
