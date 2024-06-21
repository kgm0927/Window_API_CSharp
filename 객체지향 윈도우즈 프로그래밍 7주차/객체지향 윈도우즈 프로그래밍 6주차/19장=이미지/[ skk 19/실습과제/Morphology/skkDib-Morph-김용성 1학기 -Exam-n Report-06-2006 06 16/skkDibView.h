// skkDibView.h : interface of the CSkkDibView class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_SKKDIBVIEW_H__D9D4844F_7452_11D3_B809_0000C0B11065__INCLUDED_)
#define AFX_SKKDIBVIEW_H__D9D4844F_7452_11D3_B809_0000C0B11065__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


class CSkkDibView : public CScrollView
{
protected: // create from serialization only
	CSkkDibView();
	DECLARE_DYNCREATE(CSkkDibView)

// Attributes
public:
	CSkkDibDoc* GetDocument();

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSkkDibView)
	public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	protected:
	virtual void OnInitialUpdate(); // called first time after construct
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CSkkDibView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CSkkDibView)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in skkDibView.cpp
inline CSkkDibDoc* CSkkDibView::GetDocument()
   { return (CSkkDibDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SKKDIBVIEW_H__D9D4844F_7452_11D3_B809_0000C0B11065__INCLUDED_)
