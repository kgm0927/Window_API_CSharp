// skkDibDoc.h : interface of the CSkkDibDoc class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_SKKDIBDOC_H__D9D4844D_7452_11D3_B809_0000C0B11065__INCLUDED_)
#define AFX_SKKDIBDOC_H__D9D4844D_7452_11D3_B809_0000C0B11065__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "Image.h"
class CSkkDibDoc : public CDocument
{
protected: // create from serialization only
	CSkkDibDoc();
	DECLARE_DYNCREATE(CSkkDibDoc)

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSkkDibDoc)
	public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);
	virtual BOOL OnOpenDocument(LPCTSTR lpszPathName);
	virtual BOOL OnSaveDocument(LPCTSTR lpszPathName);
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CSkkDibDoc();
	CImage m_Image;
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CSkkDibDoc)
	afx_msg void OnBClosing();
	afx_msg void OnBDilation();
	afx_msg void OnBErosion();
	afx_msg void OnBOpening();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

class CWait
{
protected:
	CSkkDibDoc *m_pDoc;

public:
	CWait(CSkkDibDoc *pDoc);
	virtual ~CWait();
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SKKDIBDOC_H__D9D4844D_7452_11D3_B809_0000C0B11065__INCLUDED_)
